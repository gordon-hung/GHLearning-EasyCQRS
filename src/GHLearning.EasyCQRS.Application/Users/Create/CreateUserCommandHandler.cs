using System.Transactions;
using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using GHLearning.EasyCQRS.SharedKernel;
using Microsoft.Extensions.Logging;

namespace GHLearning.EasyCQRS.Application.Users.Create;

internal class CreateUserCommandHandler(
	ILogger<CreateUserCommandHandler> logger,
	TimeProvider timeProvider,
	IUserCodeGenerator userCodeGenerator,
	IPasswordHasher passwordHasher,
	IUserRepository userRepository) : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
	public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
	{
		var username = command.Username.ToLower().Trim();
		if (await userRepository.ExistsAsync(username, cancellationToken).ConfigureAwait(false))
		{
			throw new InvalidOperationException($"Username '{username}' already exists.");
		}
		var code = await userCodeGenerator.NewCodeAsync(cancellationToken).ConfigureAwait(false);
		if (await userRepository.ExistsAsync(code, cancellationToken).ConfigureAwait(false))
		{
			throw new InvalidOperationException($"User code '{code}' already exists.");
		}
		var password = passwordHasher.HashPassword(command.Password);

		var operationAt = timeProvider.GetUtcNow().UtcDateTime;
		using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
		try
		{
			await userRepository.CreateAsync(
				parameter: new CreatedParameter(
					Code: code,
					Username: username,
					Password: password,
					OperationAt: operationAt),
				cancellationToken: cancellationToken)
				.ConfigureAwait(false);

			await userRepository.UpdateByRegisterAsync(
				parameter: new UpdateByRegisterParameter(
					Code: code,
					OperationAt: timeProvider.GetUtcNow()),
				cancellationToken: cancellationToken)
				.ConfigureAwait(false);

			scope.Complete();

			return new CreateUserResponse(code);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create user with username '{Username}' and code '{Code}'.", username, code);
			throw;
		}
	}
}
