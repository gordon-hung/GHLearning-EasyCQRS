namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdateByRegisterParameter(
	string Code,
	DateTimeOffset OperationAt);