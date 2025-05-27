using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Application.Users.UpdateByPassword;
internal class UpdateUserByPasswordCommandHandler(
	TimeProvider timeProvider,
	IPasswordHasher passwordHasher,
	IUserRepository userRepository) : ICommandHandler<UpdateUserByPasswordCommand>
{
	public async Task Handle(UpdateUserByPasswordCommand command, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByUsernameAsync(command.Username, cancellationToken).ConfigureAwait(false)
			?? throw new ArgumentException($"User with username '{command.Username}' does not exist.", nameof(command.Username));

		var passwordHash = passwordHasher.HashPassword(command.Password);

		var operationAt = timeProvider.GetUtcNow();

		await userRepository.UpdateByPasswordAsync(
			parameter: new UpdatedByPasswordParameter(
				Code: user.Code,
				Password: passwordHash,
				OperationAt: operationAt),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}
}