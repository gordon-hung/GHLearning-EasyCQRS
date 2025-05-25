using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Core.Users;

namespace GHLearning.EasyCQRS.Application.Users.GetByUsername;

internal class GetUserByUsernameQueryHandler(
	IUserRepository userRepository) : IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameResponse?>
{
	public async Task<GetUserByUsernameResponse?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken)
	{
		var userInfo = await userRepository.GetByUsernameAsync(query.Username, cancellationToken).ConfigureAwait(false);

		return userInfo == null
			? null
			: new GetUserByUsernameResponse(
				Code: userInfo.Code,
				Username: userInfo.Username,
				Status: userInfo.Status,
				CreatedAt: userInfo.CreatedAt,
				UpdatedAt: userInfo.UpdatedAt,
				RegisteredAt: userInfo.RegisteredAt);
	}
}
