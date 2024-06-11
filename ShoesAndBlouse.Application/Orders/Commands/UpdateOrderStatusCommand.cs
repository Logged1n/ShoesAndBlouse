using MediatR;
using ShoesAndBlouse.Domain.Enums;

namespace ShoesAndBlouse.Application.Orders.Commands;

public record UpdateOrderStatusCommand : IRequest
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
}