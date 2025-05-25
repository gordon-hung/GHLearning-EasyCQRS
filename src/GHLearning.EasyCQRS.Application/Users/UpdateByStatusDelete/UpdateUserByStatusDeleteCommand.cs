using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Users.UpdateByStatusDelete;
public record UpdateUserByStatusDeleteCommand(
	string Username) : ICommand;
