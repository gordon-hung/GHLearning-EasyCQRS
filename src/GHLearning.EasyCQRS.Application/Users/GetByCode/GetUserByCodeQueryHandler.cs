using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Core.Users;

namespace GHLearning.EasyCQRS.Application.Users.GetByCode;
internal class GetUserByCodeQueryHandler(
	IUserRepository userRepository) : IQueryHandler<GetUserByCodeQuery, GetUserByCodeResponse?>
{
	public async Task<GetUserByCodeResponse?> Handle(GetUserByCodeQuery query, CancellationToken cancellationToken)
	{
		var userInfo = await userRepository.GetByCodeAsync(query.Code, cancellationToken).ConfigureAwait(false);

		return userInfo == null
			? null
			: new GetUserByCodeResponse(
				Code: userInfo.Code,
				Username: userInfo.Username,
				Status: userInfo.Status,
				CreatedAt: userInfo.CreatedAt,
				UpdatedAt: userInfo.UpdatedAt,
				RegisteredAt: userInfo.RegisteredAt);
	}
}
