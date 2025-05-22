using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLearning.EasyCQRS.SharedKernel;
public enum UserStatus
{
	None = 0,
	Active = 1,
	Disabled = 2,
	Blocked = 3,
	Unverified = 4,
	Deleted = 5,
}