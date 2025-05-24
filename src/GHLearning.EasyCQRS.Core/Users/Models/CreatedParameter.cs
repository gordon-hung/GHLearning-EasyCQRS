namespace GHLearning.EasyCQRS.Core.Users.Models;
public record CreatedParameter(
	string Code,
	string Username,
	string Password,
	DateTimeOffset OperationAt);