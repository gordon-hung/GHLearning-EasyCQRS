namespace GHLearning.EasyCQRS.SharedKernel;

public interface ISequentialGuidGenerator
{
	Task<Guid> NewIdAsync(CancellationToken cancellationToken = default);
}
