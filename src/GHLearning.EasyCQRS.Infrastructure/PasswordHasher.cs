using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.SharedKernel;

namespace GHLearning.EasyCQRS.Infrastructure;
internal class PasswordHasher : IPasswordHasher
{
	private const int saltRounds = 10;

	public string HashPassword(string plainPassword)
	{
		// 生成隨機的鹽並哈希密碼
		string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword, saltRounds);
		return hashedPassword;
	}

	public bool VerifyPassword(string plainPassword, string hashedPassword)
	{
		// 驗證密碼是否正確
		return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
	}
}