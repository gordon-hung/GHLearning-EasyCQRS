using GHLearning.EasyCQRS.Application.Abstractions.Messaging;
using GHLearning.EasyCQRS.Application.Users.Create;
using GHLearning.EasyCQRS.Application.Users.GetByUsername;
using GHLearning.EasyCQRS.Application.Users.UpdateByPassword;
using GHLearning.EasyCQRS.Application.Users.UpdateByStatusDelete;
using GHLearning.EasyCQRS.WebApi.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GHLearning.EasyCQRS.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	[HttpPost]
	public Task<CreateUserViewModel> CreateUserAsync(
		[FromServices] ICommandHandler<CreateUserCommand, CreateUserResponse> command,
		[FromBody] CreateUserRequest request)
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

	[HttpGet("{username}")]
	public async Task<GetUserByUsernameViewModel?> GetUserByUsernameAsync(
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

	[HttpDelete("{username}")]
	public Task UpdateUserByStatusDeleteAsync(
		[FromServices] ICommandHandler<UpdateUserByStatusDeleteCommand> command,
		string username)
		=> command.Handle(
			command: new UpdateUserByStatusDeleteCommand(
				Username: username),
			cancellationToken: HttpContext.RequestAborted);

	[HttpPatch("{username}/Password")]
	public Task UpdateUserByPasswordAsync(
		[FromServices] ICommandHandler<UpdateUserByPasswordCommand> command,
		string username,
		[FromBody] UpdateUserByPasswordRequest request)
		=> command.Handle(
			command: new UpdateUserByPasswordCommand(
				Username: username,
				Password: request.Password),
			cancellationToken: HttpContext.RequestAborted);
}
