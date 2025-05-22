using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLearning.EasyCQRS.Core.Users.Models;
public record CreatedParameter(
	string Code,
	string Username,
	string Password,
	DateTimeOffset OperationAt);