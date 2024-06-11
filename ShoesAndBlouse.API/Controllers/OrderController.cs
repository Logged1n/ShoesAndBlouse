using System.Security.Claims;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.API.Helpers;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Orders.CommandHandlers;
using ShoesAndBlouse.Application.Orders.Commands;
using ShoesAndBlouse.Application.Orders.Queries;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[ApiVersion(1)]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [MapToApiVersion(1)]
    [HttpGet("GetAll")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<ActionResult<List<OrderDto>>> GetAll()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }

    [MapToApiVersion(1)]
    [Authorize(Roles = "Client, Manager, Admin")]
    [HttpGet("GetById/{orderId}")]
    public async Task<ActionResult<OrderDto>> GetById(int orderId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null && User.IsInRole("Client"))
            return Unauthorized();
        
        var order = await _mediator.Send(new GetOrderByIdQuery(orderId));
        return Ok(order);
    }

    [MapToApiVersion(1)]
    [HttpGet("GetAllUserOrders")]
    [Authorize(Roles = "Client, Manager, Admin")]
    public async Task<ActionResult<List<OrderDto>>> GetAllUserOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null && User.IsInRole("Client"))
            return Unauthorized();
        
        var orders = await _mediator.Send(new GetAllUserOrdersQuery {UserId = Convert.ToInt32(userId)});
        return Ok(orders);
    }

    [MapToApiVersion(1)]
    [HttpGet("GetAllNotCompleted")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<ActionResult<List<OrderDto>>> GetAllNotCompleted()
    {
        var orders = await _mediator.Send(new GetAllNotCompletedOrdersQuery());
        return Ok(orders);
    }

    [MapToApiVersion(1)]
    [HttpPost("MakeOrder")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> MakeOrder(MakeOrderRequestWrapper request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();
        var command = new CreateOrderCommand
        {
            UserId = Convert.ToInt32(userId),
            BillingAddress = request.BillingAddress,
            ShippingAddress = request.ShippingAddress
        };

         await _mediator.Send(command);
         return Ok();
    }

    [MapToApiVersion(1)]
    [HttpPatch("UpdateStatus")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateStatus(UpdateOrderStatusCommand command)
    {
         await _mediator.Send(command);
         return NoContent();
    }
    
    [MapToApiVersion(1)]
    [HttpPatch("Update")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<ActionResult<OrderDto>> Update(UpdateOrderCommand command)
    {
        var orderDto = await _mediator.Send(command);
        if (orderDto is not null)
            return Ok(orderDto);
        return BadRequest();
    }

    [MapToApiVersion(1)]
    [HttpDelete("Delete")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Delete(DeleteOrderCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
}