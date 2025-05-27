using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Users.GetByUsername;
public record GetUserByUsernameQuery(string Username) : IQuery<GetUserByUsernameResponse?>;
