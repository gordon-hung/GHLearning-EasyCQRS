using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Users.GetByCode;
public record GetUserByCodeQuery(
	string Code) : IQuery<GetUserByCodeResponse?>;