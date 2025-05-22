using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenTelemetry.Trace;
using GHLearning.EasyCQRS.Application.Abstractions.Tracing;
using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Abstractions.Tracing;


internal sealed class TracingCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
	where TCommand : ICommand<TResponse>
{
	private readonly ICommandHandler<TCommand, TResponse> _inner;
	private readonly Tracer _tracer;

	public TracingCommandHandler(
		ICommandHandler<TCommand, TResponse> inner,
		TracerProvider tracerProvider)
	{
		_inner = inner;
		_tracer = tracerProvider.GetTracer(
			typeof(TracingCommandHandler<TCommand, TResponse>).Assembly.GetName().Name!,
			typeof(TracingCommandHandler<TCommand, TResponse>).Assembly.GetName().Version?.ToString() ?? "1.0.0.0");
	}

	public Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
	{
		using var span = _tracer.StartActiveSpan(typeof(TCommand).Name);
		try
		{
			span.SetAttribute("TCommand", typeof(TCommand).Name);
			span.SetAttribute("TResponse", typeof(TResponse).Name);
			return _inner.Handle(command, cancellationToken);
		}
		catch (Exception ex)
		{
			span.RecordException(ex);
			span.SetStatus(Status.Error);
			throw;
		}
	}
}
