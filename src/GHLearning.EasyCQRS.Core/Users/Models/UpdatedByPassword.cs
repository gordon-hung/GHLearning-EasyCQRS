using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLearning.EasyCQRS.Core.Users.Models;
public record UpdatedByPassword(
	string Code,
	string Password,
	DateTimeOffset OperationAt);
