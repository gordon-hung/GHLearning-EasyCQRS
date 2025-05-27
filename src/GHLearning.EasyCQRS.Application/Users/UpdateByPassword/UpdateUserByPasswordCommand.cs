using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Users.UpdateByPassword;
public record UpdateUserByPasswordCommand(
	string Username,
	string Password) : ICommand;
