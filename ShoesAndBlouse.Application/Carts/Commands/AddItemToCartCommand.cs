using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.Commands;

public class AddItemToCartCommand : IRequest<CartDto>
{
    public int userId { get; set; }
    public CartItem Item { get; set; }
}