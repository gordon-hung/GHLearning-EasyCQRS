﻿using GHLearning.EasyCQRS.Core.Users.Models;

namespace GHLearning.EasyCQRS.Core.Users;

public interface IUserRepository
{
	Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default);

	Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);

	Task<UserInfo?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

	Task<UserInfo?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

	Task CreateAsync(CreatedParameter parameter, CancellationToken cancellationToken = default);

	Task UpdateByPasswordAsync(UpdatedByPasswordParameter parameter, CancellationToken cancellationToken = default);

	Task UpdateByStatusAsync(UpdatedByStatusParameter parameter, CancellationToken cancellationToken = default);

	Task UpdateByRegisterAsync(UpdateByRegisterParameter parameter, CancellationToken cancellationToken = default);

	Task DeleteAsync(DeleteParameter parameter, CancellationToken cancellationToken = default);
}
