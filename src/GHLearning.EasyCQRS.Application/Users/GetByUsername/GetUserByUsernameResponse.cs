using GHLearning.EasyCQRS.Core.Users;

namespace GHLearning.EasyCQRS.Application.Users.GetByUsername;
public record GetUserByUsernameResponse(
	string Code,
	string Username,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt);
