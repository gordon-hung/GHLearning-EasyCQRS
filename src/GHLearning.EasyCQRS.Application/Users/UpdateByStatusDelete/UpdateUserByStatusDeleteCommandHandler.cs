using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;

namespace GHLearning.EasyCQRS.Application.Users.UpdateByStatusDelete;
internal class UpdateUserByStatusDeleteCommandHandler(
	TimeProvider timeProvider,
	IUserRepository userRepository) : ICommandHandler<UpdateUserByStatusDeleteCommand>
{
	public async Task Handle(UpdateUserByStatusDeleteCommand command, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByUsernameAsync(command.Username, cancellationToken).ConfigureAwait(false)
			?? throw new ArgumentException($"User with username '{command.Username}' does not exist.", nameof(command.Username));


		if (user.Status == UserStatus.Deleted)
		{
			throw new InvalidOperationException($"User with username '{command.Username}' is already deleted.");
		}

		var operationAt = timeProvider.GetUtcNow();

		await userRepository.DeleteAsync(
			parameter: new DeleteParameter(
				Code: user.Code,
				OperationAt: operationAt),
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}
}