using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using GHLearning.EasyCQRS.Infrastructure.Entities;
using GHLearning.EasyCQRS.Infrastructure.Entities.Models;
using GHLearning.EasyCQRS.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace GHLearning.EasyCQRS.Infrastructure;

internal class UserRepository(
	EasyDbContext context) : IUserRepository
{
	public async Task CreateAsync(CreatedParameter parameter, CancellationToken cancellationToken = default)
	{
		var entity = new UserEntity
		{
			Code = parameter.Code,
			Username = parameter.Username,
			Password = parameter.Password,
			Status = UserStatus.Active,
			CreatedAt = parameter.OperationAt.UtcDateTime,
			UpdatedAt = parameter.OperationAt.UtcDateTime,
			RegisteredAt = parameter.OperationAt.UtcDateTime,
			DeletedAt = null
		};

		await context.Users.AddAsync(entity, cancellationToken).ConfigureAwait(false);
		await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}

	public Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default)
		=> context.Users.AnyAsync(u => u.Code == code, cancellationToken);


	public Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
		=> context.Users.AnyAsync(u => u.Username == username, cancellationToken);

	public async Task<UserInfo?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
	{
		var user = await context.Users
		.FirstOrDefaultAsync(u => u.Code == code, cancellationToken)
		.ConfigureAwait(false);

		return user is null
			? null
			: new UserInfo(
				Code: user.Code,
				Username: user.Username,
				Password: user.Password,
				Status: user.Status,
				CreatedAt: user.CreatedAt,
				UpdatedAt: user.UpdatedAt,
				RegisteredAt: user.RegisteredAt,
				DeletedAt: user.DeletedAt);
	}

	public async Task<UserInfo?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
	{
		var user = await context.Users
		.FirstOrDefaultAsync(u => u.Username == username, cancellationToken)
		.ConfigureAwait(false);

		return user is null
			? null
			: new UserInfo(
				Code: user.Code,
				Username: user.Username,
				Password: user.Password,
				Status: user.Status,
				CreatedAt: user.CreatedAt,
				UpdatedAt: user.UpdatedAt,
				RegisteredAt: user.RegisteredAt,
				DeletedAt: user.DeletedAt);
	}

	public async Task UpdateByPasswordAsync(UpdatedByPassword parameter, CancellationToken cancellationToken = default)
	{
		var entity = await context.Users.FirstOrDefaultAsync(u => u.Code == parameter.Code, cancellationToken).ConfigureAwait(false)
			?? throw new InvalidOperationException($"User with code {parameter.Code} not found.");

		entity.Password = parameter.Password;
		entity.UpdatedAt = parameter.OperationAt.UtcDateTime;

		context.Users.Update(entity);

		await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}

	public async Task UpdateByStatusAsync(UpdatedByStatus parameter, CancellationToken cancellationToken = default)
	{
		var entity = await context.Users.FirstOrDefaultAsync(u => u.Code == parameter.Code, cancellationToken).ConfigureAwait(false)
			?? throw new InvalidOperationException($"User with code {parameter.Code} not found.");

		entity.Status = parameter.Status;
		entity.UpdatedAt = parameter.OperationAt.UtcDateTime;

		context.Users.Update(entity);

		await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}
}
