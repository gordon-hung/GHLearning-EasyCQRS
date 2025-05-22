using GHLearning.EasyCQRS.Application.Abstractions.Behaviors;
using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Application.Abstractions.Tracing;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

namespace GHLearning.EasyCQRS.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddCQRS(typeof(ServiceCollectionExtensions), ServiceLifetime.Transient);
		services.AddCQRSOpenTelemetry();
		services.TryDecorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
		services.TryDecorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
		services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));

		return services;
	}

	public static TracerProviderBuilder AddCQRSInstrumentation(this TracerProviderBuilder builder)
	{
		builder.AddSource("GHLearning.EasyCQRS.Application");
		return builder;
	}

	private static IServiceCollection AddCQRS(this IServiceCollection services, Type type, ServiceLifetime lifetime = ServiceLifetime.Transient)
		=> services.Scan(scan => scan.FromAssembliesOf(type)
	.AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
		.AsImplementedInterfaces()
		.WithLifetime(lifetime)
	.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
		.AsImplementedInterfaces()
		.WithLifetime(lifetime)
	.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
		.AsImplementedInterfaces()
		.WithLifetime(lifetime));

	private static IServiceCollection AddCQRSOpenTelemetry(this IServiceCollection services)
	{
		services.TryDecorate(typeof(IQueryHandler<,>), typeof(TracingQueryHandler<,>));
		services.TryDecorate(typeof(ICommandHandler<,>), typeof(TracingCommandHandler<,>));
		services.TryDecorate(typeof(ICommandHandler<>), typeof(TracingBaseCommandHandler<>));
		return services;
	}
}
