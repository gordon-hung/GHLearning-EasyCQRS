using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLearning.EasyCQRS.Core.Users.Models;
public record DeleteParameter(
	string Code,
	DateTimeOffset OperationAt);
