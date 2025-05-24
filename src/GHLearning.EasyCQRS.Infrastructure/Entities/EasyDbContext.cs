using GHLearning.EasyCQRS.Infrastructure.Entities.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace GHLearning.EasyCQRS.Infrastructure.Entities;
public class EasyDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<UserEntity> Users { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<UserEntity>().ToCollection("users");
	}
}