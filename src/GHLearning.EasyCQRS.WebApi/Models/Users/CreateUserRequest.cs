namespace GHLearning.EasyCQRS.WebApi.Models.Users;

public record CreateUserRequest(
	string Username,
	string Password);
