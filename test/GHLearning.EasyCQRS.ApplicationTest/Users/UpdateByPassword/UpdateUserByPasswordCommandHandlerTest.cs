using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Application.Users.UpdateByPassword;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using GHLearning.EasyCQRS.SharedKernel;
using NSubstitute;

namespace GHLearning.EasyCQRS.ApplicationTest.Users.UpdateByPassword;
public class UpdateUserByPasswordCommandHandlerTest
{
	[Fact]
	public async Task Handle()
	{
		// Arrange
		var fakeUserRepository = NSubstitute.Substitute.For<IUserRepository>();
		var fakePasswordHasher = NSubstitute.Substitute.For<IPasswordHasher>();
		var fakeTimeProvider = NSubstitute.Substitute.For<TimeProvider>();
		var command = new UpdateUserByPasswordCommand("testuser", "newpassword");
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

		var hashedPassword = "newhashedpassword";
		fakePasswordHasher.HashPassword(command.Password).Returns(hashedPassword);
		// Act
		using var cancellationTokenSource = new CancellationTokenSource();
		var sut = new UpdateUserByPasswordCommandHandler(fakeTimeProvider, fakePasswordHasher, fakeUserRepository);
		await sut.Handle(command, cancellationTokenSource.Token);

		// Assert
		_ = fakeUserRepository
			.Received(1)
			.UpdateByPasswordAsync(
			parameter: Arg.Is<UpdatedByPasswordParameter>(p =>
			p.Code == userInfo.Code &&
			p.Password == hashedPassword),
			cancellationTokenSource.Token);
	}
}
