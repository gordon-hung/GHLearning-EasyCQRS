using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;

namespace GHLearning.EasyCQRS.Infrastructure;

internal class UserRepository : IUserRepository
{
	public Task CreateAsync(CreatedParameter parameter, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task<UserInfo?> GetByCodeAsync(string code, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task<UserInfo?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task UpdateByPasswordAsync(UpdatedByPassword parameter, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	public Task UpdateByStatusAsync(UpdatedByStatus parameter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
