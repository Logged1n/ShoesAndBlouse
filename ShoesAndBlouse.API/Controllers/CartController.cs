using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Application.Carts.Queries;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.API.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[Authorize(Roles = "Client, Manager, Admin")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    private const string SessionUserIdKey = "UserId";
    public CartController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    private int GetUserId()
    {
        // Pobierz lub stwórz identyfikator użytkownika w sesji
        if (!HttpContext.Session.TryGetValue(SessionUserIdKey, out var userIdBytes))
        {
            var userId = new Random().Next(1000, 9999); // Przykładowe generowanie userId
            HttpContext.Session.SetInt32(SessionUserIdKey, userId);
            return userId;
        }

        return HttpContext.Session.GetInt32(SessionUserIdKey) ?? 0;
    }

    [MapToApiVersion(1)]
    [HttpGet("Get")]
    public async Task<IActionResult> GetCart()
    {
        var userId = GetUserId();
        var query = new GetCartQuery { userId = userId };
        return Ok(await _mediator.Send(query));
    }

    [MapToApiVersion(1)]
    [HttpPost("Add")]
    public async Task<IActionResult> AddItem(CartItem item)
    {
        int userId = GetUserId();
        var command = new AddItemToCartCommand { Item = item, userId = userId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [MapToApiVersion(1)]
    [HttpPost("Remove")]
    public async Task<IActionResult> RemoveItem(CartItem item)
    {
        int userId = GetUserId();
        var command = new RemoveItemFromCartCommand { item = item, userId = userId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}