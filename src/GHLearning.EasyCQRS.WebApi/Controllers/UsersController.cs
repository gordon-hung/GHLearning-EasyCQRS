using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Application.Users.Create;
using GHLearning.EasyCQRS.Application.Users.GetByCode;
using GHLearning.EasyCQRS.Application.Users.GetByUsername;
using GHLearning.EasyCQRS.SharedKernel;
using GHLearning.EasyCQRS.WebApi.Models.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace GHLearning.EasyCQRS.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	[HttpPost]
	public Task<CreateUserViewModel> CreateUserAsync(
		[FromServices] ICommandHandler<CreateUserCommand, CreateUserResponse> command,
		[FromBody] CreateUserRrequest request)
	{
		if (request is null)
		{
			throw new ArgumentNullException(nameof(request), "Request cannot be null.");
		}

		return command.Handle(
			command: new CreateUserCommand(
			Username: request.Username,
			Password: request.Password),
			cancellationToken: HttpContext.RequestAborted)
			.ContinueWith(task => new CreateUserViewModel(task.Result.Code), HttpContext.RequestAborted);
	}

	[HttpGet("{code}/Code/Info")]
	public async Task<GetUserByCodeViewModel?> GetUserByCodeAsync(
	[FromServices] IQueryHandler<GetUserByCodeQuery, GetUserByCodeResponse?> query,
	string code)
	{
		var user = await query.Handle(
			query: new GetUserByCodeQuery(code),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return user is null ? null : new GetUserByCodeViewModel(
			Code: user.Code,
			Username: user.Username,
			Status: user.Status,
			CreatedAt: user.CreatedAt,
			UpdatedAt: user.UpdatedAt,
			RegisteredAt: user.RegisteredAt);
	}

	[HttpGet("{username}/Username/Info")]
	public async Task<GetUserByUsernameViewModel?> GetUserByCodeAsync(
	[FromServices] IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameResponse?> query,
	string username)
	{
		var user = await query.Handle(
			query: new GetUserByUsernameQuery(username),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return user is null ? null : new GetUserByUsernameViewModel(
			Code: user.Code,
			Username: user.Username,
			Status: user.Status,
			CreatedAt: user.CreatedAt,
			UpdatedAt: user.UpdatedAt,
			RegisteredAt: user.RegisteredAt);
	}
}
