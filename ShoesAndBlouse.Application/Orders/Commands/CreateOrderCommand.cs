using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Orders.Commands;

public record CreateOrderCommand : IRequest
{
    public int UserId { get; set; }
    public AddressDto? BillingAddress { get; set; }
    public AddressDto? ShippingAddress { get; set; }
    
}