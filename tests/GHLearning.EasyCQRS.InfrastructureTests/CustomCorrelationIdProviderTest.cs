using CorrelationId;
using GHLearning.EasyCQRS.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace GHLearning.EasyCQRS.InfrastructureTests;
public class CustomCorrelationIdProviderTest
{
	[Fact]
	public void GenerateCorrelationId_ReturnsHeaderValue_WhenHeaderExists()
	{
		// Arrange
		var context = new DefaultHttpContext();
		var expected = "header-correlation-id";
		context.Request.Headers[CorrelationIdOptions.DefaultHeader] = expected;
		var sut = new CustomCorrelationIdProvider();

		// Act
		var result = sut.GenerateCorrelationId(context);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void GenerateCorrelationId_ReturnsItemValue_WhenHeaderMissingAndItemExists()
	{
		// Arrange
		var context = new DefaultHttpContext();
		var expected = "item-correlation-id";
		context.Items[CorrelationIdOptions.DefaultHeader] = expected;
		var provider = new CustomCorrelationIdProvider();

		// Act
		var result = provider.GenerateCorrelationId(context);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void GenerateCorrelationId_UsesSequentialGuid_WhenHeaderAndItemMissing()
	{
		// Arrange
		var context = new DefaultHttpContext();
		var provider = new CustomCorrelationIdProvider();

		// Act
		var result = provider.GenerateCorrelationId(context);

		// Assert
		Assert.NotNull(result);
	}
}
