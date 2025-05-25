using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Application.Users.GetByCode;
public record GetUserByCodeResponse(
	string Code,
	string Username,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt);
