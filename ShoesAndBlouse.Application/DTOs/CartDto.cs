using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.DTOs;

public class CartDto
{
    public List<CartItem> OrderDetails { get; set; } = [];
}