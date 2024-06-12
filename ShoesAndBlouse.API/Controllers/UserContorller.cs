using System.Security.Claims;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Users.Commands;
using ShoesAndBlouse.Application.Users.Queries;

namespace ShoesAndBlouse.API.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


    [MapToApiVersion(1)]
    [HttpGet("GetId")]
    public async Task<ActionResult<string>> GetId()
    {
        return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
    
    [MapToApiVersion(1)]
    [HttpGet("GetById/{userId}")]
    public async Task<ActionResult<UserDto?>> GetById(int userId)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = userId });
        return Ok(user);
    }

    [MapToApiVersion(1)]
    [Authorize(Roles = "Admin, Manager")]
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
    [Authorize(Roles = "Admin, Manager, Client")]
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
