using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.WebApi.Models.Users;

public record GetUserByCodeViewModel(
	string Code,
	string Username,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt,
	DateTimeOffset? DeletedAt);
