using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

using Microsoft.Extensions.Logging;

namespace GHLearning.EasyCQRS.Application.Abstractions.Behaviors;

internal static class LoggingDecorator
{
	internal sealed class CommandHandler<TCommand, TResponse>(
		ICommandHandler<TCommand, TResponse> innerHandler,
		ILogger<CommandHandler<TCommand, TResponse>> logger)
		: ICommandHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
		public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
		{
			string commandName = typeof(TCommand).Name;

			logger.LogInformation("Processing command {Command}", commandName);

			try
			{
				TResponse result = await innerHandler.Handle(command, cancellationToken).ConfigureAwait(false);
				logger.LogInformation("Completed command {Command}", commandName);
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Completed command {Command} with error", commandName);
				throw;
			}
		}
	}

	internal sealed class CommandBaseHandler<TCommand>(
		ICommandHandler<TCommand> innerHandler,
		ILogger<CommandBaseHandler<TCommand>> logger)
		: ICommandHandler<TCommand>
		where TCommand : ICommand
	{
		public async Task Handle(TCommand command, CancellationToken cancellationToken)
		{
			string commandName = typeof(TCommand).Name;

			logger.LogInformation("Processing command {Command}", commandName);

			try
			{
				await innerHandler.Handle(command, cancellationToken).ConfigureAwait(false);
				logger.LogInformation("Completed command {Command}", commandName);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Completed command {Command} with error", commandName);
				throw;
			}
		}
	}

	internal sealed class QueryHandler<TQuery, TResponse>(
		IQueryHandler<TQuery, TResponse> innerHandler,
		ILogger<QueryHandler<TQuery, TResponse>> logger)
		: IQueryHandler<TQuery, TResponse>
		where TQuery : IQuery<TResponse>
	{
		public async Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
		{
			string queryName = typeof(TQuery).Name;

			logger.LogInformation("Processing query {Query}", queryName);

			try
			{
				TResponse result = await innerHandler.Handle(query, cancellationToken).ConfigureAwait(false);
				logger.LogInformation("Completed query {Query}", queryName);
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Completed query {Query} with error", queryName);
				throw;
			}
		}
	}
}
