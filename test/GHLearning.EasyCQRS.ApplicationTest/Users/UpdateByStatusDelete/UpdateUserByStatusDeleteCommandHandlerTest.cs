using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Application.Users.UpdateByStatusDelete;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using NSubstitute;

namespace GHLearning.EasyCQRS.ApplicationTest.Users.UpdateByStatusDelete;
public class UpdateUserByStatusDeleteCommandHandlerTest
{
	[Fact]
	public async Task Handle()
	{
		// Arrange
		var fakeUserRepository = NSubstitute.Substitute.For<IUserRepository>();
		var fakeTimeProvider = NSubstitute.Substitute.For<TimeProvider>();
		var command = new UpdateUserByStatusDeleteCommand("testuser");
		var userInfo = new UserInfo(
			Code: "testcode",
			Username: "testuser",
			Password: "hashedpassword",
			Status: UserStatus.Active,
			CreatedAt: DateTimeOffset.UtcNow.AddDays(-1),
			UpdatedAt: DateTimeOffset.UtcNow,
			RegisteredAt: DateTimeOffset.UtcNow.AddDays(-1),
			DeletedAt: null);
		fakeUserRepository.GetByUsernameAsync(command.Username, Arg.Any<CancellationToken>())
			.Returns(Task.FromResult<UserInfo?>(userInfo));
		fakeTimeProvider.GetUtcNow().Returns(DateTimeOffset.UtcNow);

		// Act
		using var cancellationTokenSource = new CancellationTokenSource();
		var sut = new UpdateUserByStatusDeleteCommandHandler(fakeTimeProvider, fakeUserRepository);
		await sut.Handle(command, cancellationTokenSource.Token);

		// Assert
		await fakeUserRepository
			.Received(1)
			.DeleteAsync(
			parameter: Arg.Is<DeleteParameter>(p =>
			p.Code == userInfo.Code),
			cancellationTokenSource.Token);
	}
}
