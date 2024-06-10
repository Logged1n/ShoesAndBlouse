using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesAndBlouse.Application.Users.Commands;
using ShoesAndBlouse.Application.Users.Queries;

using ShoesAndBlouse.Application.Users.Queries;

namespace ShoesAndBlouse.API.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [MapToApiVersion(1)]
    [AllowAnonymous]
    [HttpGet("GetById/{userId}")]
    public async Task<IActionResult> GetById(int userId)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = userId });
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [MapToApiVersion(1)]
    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [MapToApiVersion(1)]
    [HttpDelete("Delete/{userId}")]
    public async Task<IActionResult> Delete(int userId)
    {
        var result = await _mediator.Send(new DeleteUserCommand(userId));
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [MapToApiVersion(1)]
    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var validationResult = await new UpdateUserCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}
