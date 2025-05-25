using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Application.Users.Create;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using GHLearning.EasyCQRS.SharedKernel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace GHLearning.EasyCQRS.ApplicationTest.Users.Create;

public class CreateUserCommandHandlerTest
{
	[Fact]
	public async Task Handle()
	{
		// Arrange
		var fakeLogger = NullLogger<CreateUserCommandHandler>.Instance;
		var fakeTimeProvider = NSubstitute.Substitute.For<TimeProvider>();
		var fakeUserCodeGenerator = NSubstitute.Substitute.For<IUserCodeGenerator>();
		var fakePasswordHasher = NSubstitute.Substitute.For<IPasswordHasher>();
		var fakeUserRepository = NSubstitute.Substitute.For<IUserRepository>();

		var command = new CreateUserCommand(
			Username: "username",
			Password: "password");

		_ = fakeUserRepository
			.ExistsByUsernameAsync(
			username: Arg.Is(command.Username),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(false);

		var code = "code";
		_ = fakeUserCodeGenerator
			.NewCodeAsync(
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(code);

		_ = fakeUserRepository
			.ExistsAsync(
			code: Arg.Is(code),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(false);

		var hashedPassword = "hashedPassword";
		_ = fakePasswordHasher
			.HashPassword(
			plainPassword: Arg.Is(command.Password))
			.Returns(hashedPassword);

		var operationAt = DateTimeOffset.UtcNow;
		_ = fakeTimeProvider
			.GetUtcNow()
			.Returns(operationAt);

		// Act
		var sut = new CreateUserCommandHandler(
			logger: fakeLogger,
			timeProvider: fakeTimeProvider,
			userCodeGenerator: fakeUserCodeGenerator,
			passwordHasher: fakePasswordHasher,
			userRepository: fakeUserRepository);
		var actual = await sut.Handle(command, CancellationToken.None).ConfigureAwait(false);

		// Assert
		Assert.NotNull(actual);
		Assert.Equal(code, actual.Code);

		_ = fakeUserRepository
			.Received(1)
			.CreateAsync(
				parameter: Arg.Is<CreatedParameter>(p =>
					p.Code == code &&
					p.Username == command.Username.ToLower().Trim() &&
					p.Password == hashedPassword &&
					p.OperationAt == operationAt.UtcDateTime),
				cancellationToken: Arg.Any<CancellationToken>());

		_ = fakeUserRepository
			.Received(1)
			.UpdateByRegisterAsync(
				parameter: Arg.Is<UpdateByRegisterParameter>(p => p.Code == code),
				cancellationToken: Arg.Any<CancellationToken>());
	}
}
