using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers;

public static class CartMapper
{
    public static CartDto MapToDto(Cart? cart)
    {
        if (cart is not null)
        {
            return new CartDto
            {
                OrderDetails = cart.CartItems
            };
        }
        return new CartDto();
    }
}