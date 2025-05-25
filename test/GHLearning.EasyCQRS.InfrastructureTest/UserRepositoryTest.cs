using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Core.Users.Models;
using GHLearning.EasyCQRS.Infrastructure;
using GHLearning.EasyCQRS.Infrastructure.Entities;
using GHLearning.EasyCQRS.Infrastructure.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GHLearning.EasyCQRS.InfrastructureTest;
public class UserRepositoryTest
{
	[Fact]
	public async Task CreateAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(CreateAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var parameter = new CreatedParameter(
			Code: "code",
			Username: "Username",
			Password: "password",
			OperationAt: DateTimeOffset.UtcNow);

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		await sut.CreateAsync(parameter, tokenSource.Token);

		var actual = await context.Users
		.Where(x => x.Code == parameter.Code)
		.SingleAsync();

		Assert.NotNull(actual);
		Assert.Equal(parameter.Code, actual.Code);
		Assert.Equal(parameter.Username.ToLower(), actual.Username);
		Assert.NotNull(actual.CreatedAt);
		Assert.NotNull(actual.UpdatedAt);
		Assert.Equal(UserStatus.Register, actual.Status);
		Assert.Null(actual.RegisteredAt);
		Assert.Null(actual.DeletedAt);
	}

	[Fact]
	public async Task ExistsAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(ExistsAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Active,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = DateTimeOffset.UtcNow.UtcDateTime,
			DeletedAt = null
		};

		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);

		using var tokenSource = new CancellationTokenSource();
		var actual = await sut.ExistsAsync(user.Code, tokenSource.Token);
		Assert.True(actual);
	}

	[Fact]
	public async Task ExistsByUsernameAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(ExistsByUsernameAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Active,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = DateTimeOffset.UtcNow.UtcDateTime,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var actual = await sut.ExistsByUsernameAsync(user.Username, tokenSource.Token);

		Assert.True(actual);
	}

	[Fact]
	public async Task GetByCodeAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(GetByCodeAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Active,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = DateTimeOffset.UtcNow.UtcDateTime,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var actual = await sut.GetByCodeAsync(user.Code, tokenSource.Token);

		Assert.NotNull(actual);
		Assert.Equal(user.Code, actual.Code);
		Assert.Equal(user.Username.ToLower(), actual.Username);
		Assert.Equal(UserStatus.Active, actual.Status);
		Assert.NotNull(actual.CreatedAt);
		Assert.NotNull(actual.UpdatedAt);
		Assert.NotNull(actual.RegisteredAt);
		Assert.Null(actual.DeletedAt);
	}

	[Fact]
	public async Task GetByUsernameAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(GetByUsernameAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Active,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = DateTimeOffset.UtcNow.UtcDateTime,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var actual = await sut.GetByUsernameAsync(user.Username, tokenSource.Token);

		Assert.NotNull(actual);
		Assert.Equal(user.Code, actual.Code);
		Assert.Equal(user.Username.ToLower(), actual.Username);
		Assert.Equal(UserStatus.Active, actual.Status);
		Assert.NotNull(actual.CreatedAt);
		Assert.NotNull(actual.UpdatedAt);
		Assert.NotNull(actual.RegisteredAt);
		Assert.Null(actual.DeletedAt);
	}

	[Fact]
	public async Task UpdateByPasswordAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(UpdateByPasswordAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Active,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = DateTimeOffset.UtcNow.UtcDateTime,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var updateParameter = new UpdatedByPasswordParameter(
			Code: user.Code,
			Password: "newpassword",
			OperationAt: DateTimeOffset.UtcNow);
		await sut.UpdateByPasswordAsync(updateParameter, tokenSource.Token);
		var actual = await sut.GetByCodeAsync(user.Code, tokenSource.Token);

		Assert.NotNull(actual);
		Assert.Equal(updateParameter.Password, actual.Password);
	}

	[Fact]
	public async Task UpdateByRegisterAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(UpdateByRegisterAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Register,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = null,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var updateParameter = new UpdateByRegisterParameter(
			Code: user.Code,
			OperationAt: DateTimeOffset.UtcNow);
		await sut.UpdateByRegisterAsync(updateParameter, tokenSource.Token);
		var actual = await sut.GetByCodeAsync(user.Code, tokenSource.Token);

		Assert.NotNull(actual);
		Assert.Equal(UserStatus.Active, actual.Status);
		Assert.NotNull(actual.RegisteredAt);
		Assert.Null(actual.DeletedAt);
	}

	[Fact]
	public async Task UpdateByStatusAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(UpdateByStatusAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Register,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = null,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var updateParameter = new UpdatedByStatusParameter(
			Code: user.Code,
			Status: UserStatus.Blocked,
			OperationAt: DateTimeOffset.UtcNow);
		await sut.UpdateByStatusAsync(updateParameter, tokenSource.Token);
		var actual = await sut.GetByCodeAsync(user.Code, tokenSource.Token);

		Assert.NotNull(actual);
		Assert.Equal(UserStatus.Blocked, actual.Status);
	}

	[Fact]
	public async Task DeleteAsync()
	{
		var options = new DbContextOptionsBuilder<EasyDbContext>()
			.UseInMemoryDatabase(databaseName: $"dbo.{nameof(DeleteAsync)}")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
		var context = new EasyDbContext(options);
		_ = context.Database.EnsureDeleted();
		_ = context.Database.EnsureCreated();

		var user = new UserEntity
		{
			Code = "code",
			Username = "username",
			Password = "password",
			Status = UserStatus.Register,
			CreatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			UpdatedAt = DateTimeOffset.UtcNow.UtcDateTime,
			RegisteredAt = null,
			DeletedAt = null
		};
		_ = await context.Users.AddAsync(user);
		_ = await context.SaveChangesAsync();

		var sut = new UserRepository(context);
		using var tokenSource = new CancellationTokenSource();
		var deleteParameter = new DeleteParameter(
			Code: user.Code,
			OperationAt: DateTimeOffset.UtcNow);
		await sut.DeleteAsync(deleteParameter, tokenSource.Token);
		var actual = await sut.GetByCodeAsync(user.Code, tokenSource.Token);

		Assert.NotNull(actual);
		Assert.Equal(UserStatus.Deleted, actual.Status);
		Assert.NotNull(actual.DeletedAt);
	}
}
