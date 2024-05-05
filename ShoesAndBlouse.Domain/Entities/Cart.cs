using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.Entities;

[NotMapped]
public sealed class Cart
{
    public User User { get; set; }
    public List<OrderDetail> CartItems { get; set; } = [];
}