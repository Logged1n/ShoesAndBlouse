using MediatR;

namespace ShoesAndBlouse.Application.Orders.Commands;

public record DeleteOrderCommand : IRequest
{
    public int orderId { get; set; }
}