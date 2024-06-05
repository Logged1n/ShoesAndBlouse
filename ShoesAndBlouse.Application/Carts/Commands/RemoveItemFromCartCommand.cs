using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.Commands;

public class RemoveItemFromCartCommand : IRequest<CartDto>
{
    public int userId { get; set; }
    public CartItem item { get; set; }
}