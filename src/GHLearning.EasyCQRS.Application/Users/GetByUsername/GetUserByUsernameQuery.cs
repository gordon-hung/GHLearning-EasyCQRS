using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Users.GetByUsername;
public record GetUserByUsernameQuery(string Username) : IQuery<GetUserByUsernameResponse?>;
