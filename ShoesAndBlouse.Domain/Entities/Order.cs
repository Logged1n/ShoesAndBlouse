using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public OrderStatus Status { get; set; }
    public User User { get; set; }
    public Address? ShippingAddress { get; set; }
    public Address BillingAddress { get; set; }
    public List<OrderDetail> ProductsQty { get; set; }
    public Money Total { get; set; }
}