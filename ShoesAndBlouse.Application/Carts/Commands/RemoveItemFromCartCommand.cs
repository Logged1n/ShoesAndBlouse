using MediatR;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.Commands;

public class RemoveItemFromCartCommand : IRequest<Cart>
{
    public int userId { get; set; }
    public CartItem item { get; set; }
}