using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdatedByStatus(
	string Code,
	UserStatus Status);
