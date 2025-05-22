using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Core.Users.Models;

namespace GHLearning.EasyCQRS.Core.Users;

public interface IUserRepository
{
	Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default);
	Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);
	Task<UserInfo?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
	Task<UserInfo?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
	Task CreateAsync(CreatedParameter parameter, CancellationToken cancellationToken = default);
	Task UpdateByPasswordAsync(UpdatedByPassword parameter, CancellationToken cancellationToken = default);
	Task UpdateByStatusAsync(UpdatedByStatus parameter, CancellationToken cancellationToken = default);
}
