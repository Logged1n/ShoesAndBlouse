using System.ComponentModel.DataAnnotations.Schema;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Entities;

[NotMapped]
public sealed class Cart
{
    public HashSet<CartItem> CartItems { get; set; } = [];
}