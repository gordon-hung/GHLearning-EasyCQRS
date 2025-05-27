using GHLearning.EasyCQRS.Application.Users.GetByUsername;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using NSubstitute;

namespace GHLearning.EasyCQRS.ApplicationTests.Users.GetByUsername;
public class GetUserByUsernameQueryHandlerTest
{
	[Fact]
	public async Task Handle()
	{
		// Arrange
		var fakeUserRepository = NSubstitute.Substitute.For<IUserRepository>();
		var query = new GetUserByUsernameQuery("testuser");
		var userInfo = new UserInfo(
			Code: "testcode",
			Username: "testuser",
			Password: "hashedpassword",
			Status: UserStatus.Active,
			CreatedAt: DateTimeOffset.UtcNow.AddDays(-1),
			UpdatedAt: DateTimeOffset.UtcNow,
			RegisteredAt: DateTimeOffset.UtcNow.AddDays(-1),
			DeletedAt: null);
		_ = fakeUserRepository.GetByUsernameAsync(query.Username, Arg.Any<CancellationToken>())
			.Returns(Task.FromResult<UserInfo?>(userInfo));

		// Act
		using var ancellationTokenSource = new CancellationTokenSource();
		var sut = new GetUserByUsernameQueryHandler(fakeUserRepository);
		var result = await sut.Handle(query, ancellationTokenSource.Token);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(userInfo.Code, result.Code);
		Assert.Equal(userInfo.Username, result.Username);
		Assert.Equal(userInfo.Status, result.Status);
		Assert.Equal(userInfo.CreatedAt, result.CreatedAt);
		Assert.Equal(userInfo.UpdatedAt, result.UpdatedAt);
		Assert.Equal(userInfo.RegisteredAt, result.RegisteredAt);
	}
}
