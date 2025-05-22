using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		.AddDbContext<EasyDbContext>(dbContextOptions)
		.AddSingleton<IPasswordHasher, PasswordHasher>()
		.AddSingleton<ISequentialGuidGenerator, SequentialGuidGenerator>()
		.AddSingleton<IUserCodeGenerator, UserCodeGenerator>()
		.AddTransient<IUserRepository, UserRepository>();

}
