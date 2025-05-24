namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdatedByPassword(
	string Code,
	string Password,
	DateTimeOffset OperationAt);
