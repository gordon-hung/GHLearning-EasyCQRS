using GHLearning.EasyCQRS.Infrastructure;
using NSubstitute;

namespace GHLearning.EasyCQRS.InfrastructureTests;
public class UserCodeGeneratorTest
{
	[Fact]
	public async Task NewCode()
	{
		var fakeTimeProvider = Substitute.For<TimeProvider>();

		_ = fakeTimeProvider.GetUtcNow().Returns(DateTimeOffset.UtcNow);
		var sut = new UserCodeGenerator(fakeTimeProvider);
		using var tokenSource = new CancellationTokenSource();
		var code = await sut.NewCodeAsync(tokenSource.Token);
		Assert.NotEmpty(code);
		Assert.Matches("^[a-z0-9]+$", code); // Check if the code is alphanumeric and lowercase
	}
}
