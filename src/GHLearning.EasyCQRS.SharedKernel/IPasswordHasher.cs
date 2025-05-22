namespace GHLearning.EasyCQRS.SharedKernel;

public interface IPasswordHasher
{
	public string HashPassword(string plainPassword);

	public bool VerifyPassword(string plainPassword, string hashedPassword);
}
