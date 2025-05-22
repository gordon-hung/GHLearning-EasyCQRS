using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenTelemetry.Trace;
using GHLearning.EasyCQRS.Application.Abstractions.Tracing;
using GHLearning.EasyCQRS.Application.Abstractions.Messaging;

namespace GHLearning.EasyCQRS.Application.Abstractions.Tracing;

internal sealed class TracingBaseCommandHandler<TCommand> : ICommandHandler<TCommand>
	where TCommand : ICommand
{
	private readonly ICommandHandler<TCommand> _inner;
	private readonly Tracer _tracer;

	public TracingBaseCommandHandler(
		ICommandHandler<TCommand> inner,
		TracerProvider tracerProvider)
	{
		_inner = inner;
		_tracer = tracerProvider.GetTracer(
			typeof(TracingBaseCommandHandler<TCommand>).Assembly.GetName().Name!,
			typeof(TracingBaseCommandHandler<TCommand>).Assembly.GetName().Version?.ToString() ?? "1.0.0.0");
	}

	public Task Handle(TCommand command, CancellationToken cancellationToken)
	{
		using var span = _tracer.StartActiveSpan(typeof(TCommand).Name);
		try
		{
			span.SetAttribute("TCommand", typeof(TCommand).Name);
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
