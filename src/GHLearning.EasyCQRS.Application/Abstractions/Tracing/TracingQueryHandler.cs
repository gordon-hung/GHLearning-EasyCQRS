using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

using OpenTelemetry.Trace;

namespace GHLearning.EasyCQRS.Application.Abstractions.Tracing;

internal sealed class TracingQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
	where TQuery : IQuery<TResponse>
{
	private readonly IQueryHandler<TQuery, TResponse> _inner;
	private readonly Tracer _tracer;

	public TracingQueryHandler(
		IQueryHandler<TQuery, TResponse> inner,
		TracerProvider tracerProvider)
	{
		_inner = inner;
		_tracer = tracerProvider.GetTracer(
			typeof(TracingQueryHandler<TQuery, TResponse>).Assembly.GetName().Name!,
			typeof(TracingQueryHandler<TQuery, TResponse>).Assembly.GetName().Version?.ToString() ?? "1.0.0.0");
	}

	public Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
	{
		using var span = _tracer.StartActiveSpan(typeof(TQuery).Name);
		try
		{
			span.SetAttribute("TQuery", typeof(TQuery).Name);
			span.SetAttribute("TResponse", typeof(TResponse).Name);
			return _inner.Handle(query, cancellationToken);
		}
		catch (Exception ex)
		{
			span.RecordException(ex);
			span.SetStatus(Status.Error);
			throw;
		}
	}
}
