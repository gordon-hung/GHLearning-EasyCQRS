using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Users.Create;
public record CreateUserCommand(
	string Username,
	string Password) : ICommand<CreateUserResponse>;
