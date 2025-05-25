namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdatedByPasswordParameter(
	string Code,
	string Password,
	DateTimeOffset OperationAt);
