using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Infrastructure;

internal class UserCodeGenerator(
	TimeProvider timeProvider) : IUserCodeGenerator
{
	private readonly SemaphoreSlim _locker = new(1, 1);

	public async Task<string> NewCodeAsync(CancellationToken cancellationToken = default)
	{
		await _locker.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			await Task.Delay(1, cancellationToken).ConfigureAwait(false);
			long ticks = timeProvider.GetUtcNow().Ticks;
			return ConvertToBase(ticks, 36);
		}
		finally
		{
			_ = _locker.Release();
		}
	}

	private static String ConvertToBase(long num, int nbase)
	{
		String chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		// check if we can convert to another base
		if (nbase < 2 || nbase > chars.Length)
			return "";

		long r;
		String newNumber = "";

		// in r we have the offset of the char that was converted to the new base
		while (num >= nbase)
		{
			r = num % nbase;
			newNumber = chars[(int)r] + newNumber;
			num /= nbase;
		}
		// the last number to convert
		newNumber = chars[(int)num] + newNumber;

		return newNumber.ToLower();
	}
}
