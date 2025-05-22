using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.SharedKernel;

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