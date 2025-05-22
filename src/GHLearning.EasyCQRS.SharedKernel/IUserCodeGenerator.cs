namespace GHLearning.EasyCQRS.SharedKernel;

public interface IUserCodeGenerator
{
	Task<string> NewCodeAsync(CancellationToken cancellationToken = default);
}
