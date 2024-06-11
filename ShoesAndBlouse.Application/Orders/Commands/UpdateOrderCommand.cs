using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Orders.Commands;

public record UpdateOrderCommand : IRequest<OrderDto?>
{
    public int OrderId { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public AddressDto? ShippingAddress { get; set; }
    public AddressDto? BillingAddress { get; set; }
    public List<CartItem>? OrderDetails { get; set; }
}