using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Application.Users.GetByUsername;
public record GetUserByUsernameResponse(
	string Code,
	string Username,
	UserStatus Status,
	DateTimeOffset CreatedAt,
	DateTimeOffset UpdatedAt,
	DateTimeOffset? RegisteredAt);
