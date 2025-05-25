using CorrelationId;
using CorrelationId.DependencyInjection;
using GHLearning.EasyCQRS.Core.Users;
using GHLearning.EasyCQRS.Infrastructure.Entities;
using GHLearning.EasyCQRS.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GHLearning.EasyCQRS.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		Action<IServiceProvider, DbContextOptionsBuilder> dbContextOptions)
		=> services
		.AddSingleton(TimeProvider.System)
		.AddDbContextInfrastructure<EasyDbContext>(dbContextOptions)
		.AddServicesInfrastructure()
		.AddCorrelationInfrastructure();

	private static IServiceCollection AddDbContextInfrastructure<TContext>(
		this IServiceCollection services,
		Action<IServiceProvider, DbContextOptionsBuilder> dbContextOptions)
		where TContext : DbContext
		=> services.AddDbContext<TContext>(dbContextOptions)
		.AddTransient<IUserRepository, UserRepository>();

	private static IServiceCollection AddServicesInfrastructure(this IServiceCollection services)
	=> services
		.AddSingleton<IUserCodeGenerator, UserCodeGenerator>()
		.AddSingleton<IPasswordHasher, PasswordHasher>()
		.AddSingleton<ISequentialGuidGenerator, SequentialGuidGenerator>();

	private static IServiceCollection AddCorrelationInfrastructure(this IServiceCollection services)
		=> services.AddCorrelationId<CustomCorrelationIdProvider>(options =>
		{
			//Learn more about configuring CorrelationId at https://github.com/stevejgordon/CorrelationId/wiki
			options.AddToLoggingScope = true;
			options.LoggingScopeKey = CorrelationIdOptions.DefaultHeader;
		})
		.Services;
}
