using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLearning.EasyCQRS.InfrastructureTest;
public class SequentialGuidGeneratorTest
{
	[Fact]
	public async Task NewId()
	{
		var sut = new GHLearning.EasyCQRS.Infrastructure.SequentialGuidGenerator();
		using var tokenSource = new CancellationTokenSource();
		var guid = await sut.NewIdAsync(tokenSource.Token);
		Assert.NotEqual(Guid.Empty, guid);
	}
}
