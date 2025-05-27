using GHLearning.EasyCQRS.Core.Users;

namespace GHLearning.EasyCQRS.WebApi.Models.Users;

public record GetUserByUsernameViewModel(
	string Code,
	string Username,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt);