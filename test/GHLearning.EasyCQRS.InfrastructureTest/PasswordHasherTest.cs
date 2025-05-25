using GHLearning.EasyCQRS.Infrastructure;

namespace GHLearning.EasyCQRS.InfrastructureTest;

public class PasswordHasherTest
{
	[Fact]
	public void HashPassword()
	{
		var sut = new PasswordHasher();

		var hasedPassword = sut.HashPassword("testPassword123");

		Assert.NotNull(hasedPassword);
	}


	[Fact]
	public void VerifyPassword()
	{
		var sut = new PasswordHasher();

		var hasedPassword = sut.HashPassword("testPassword123");

		var isVerified = sut.VerifyPassword("testPassword123", hasedPassword);

		Assert.True(isVerified);
	}
}
