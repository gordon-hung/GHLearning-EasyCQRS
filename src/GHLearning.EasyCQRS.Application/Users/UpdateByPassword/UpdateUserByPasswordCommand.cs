using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Application.Users.Create;

namespace GHLearning.EasyCQRS.Application.Users.UpdateByPassword;
public record UpdateUserByPasswordCommand(
	string Username,
	string Password) : ICommand;
