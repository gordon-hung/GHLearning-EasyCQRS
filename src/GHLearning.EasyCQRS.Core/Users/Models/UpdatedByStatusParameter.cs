namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdatedByStatusParameter(
	string Code,
	UserStatus Status,
	DateTimeOffset OperationAt);
