namespace GHLearning.EasyCQRS.Core.Users.Models;
public record DeleteParameter(
	string Code,
	DateTimeOffset OperationAt);
