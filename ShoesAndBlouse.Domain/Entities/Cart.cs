using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.Entities;

[NotMapped]
public sealed class Cart
{
    public User? User { get; set; }
    public List<OrderDetail> CartItems { get; set; } //OrderDetails in a single cart should be unique, so HashSet is the best option to store them since Add/Remove are O(1)

    public Cart(User? user)
    {
        User = user;
        CartItems = [];
    }
}