using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Infrastructure;

internal sealed class SequentialGuidGenerator : ISequentialGuidGenerator
{
	private readonly SemaphoreSlim _locker = new(1, 1);

	public async Task<Guid> NewIdAsync(CancellationToken cancellationToken = default)
	{
		await _locker.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			return SequentialGuid.SequentialGuidGenerator.Instance.NewGuid();
		}
		finally
		{
			_ = _locker.Release();
		}
	}
}
