namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UserInfo(
	string Code,
	string Username,
	string Password,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt,
	DateTimeOffset? DeletedAt);
