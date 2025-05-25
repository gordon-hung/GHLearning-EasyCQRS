using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.WebApi.Models.Users;

public record GetUserByUsernameViewModel(
	string Code,
	string Username,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt);