using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdatedByStatus(
	string Code,
	UserStatus Status,
	DateTimeOffset OperationAt);
