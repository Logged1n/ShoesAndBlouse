using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Application.Carts.Queries;

namespace ShoesAndBlouse.API.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [MapToApiVersion(1)]
    [HttpGet("Get/{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var query = new GetCartQuery { userId = userId };
        return Ok(await _mediator.Send(query));
    }

    [MapToApiVersion(1)]
    [HttpPut("Add")]
    public async Task<IActionResult> AddItem(AddItemToCartCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [MapToApiVersion(1)]
    [HttpPut("Remove")]
    public async Task<IActionResult> RemoveItem(RemoveItemFromCartCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}