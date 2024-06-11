using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class OrderDetail
{
    [Key]
    public int Id { get; init; }
    public Order Order { get; set; }
    public Product Product { get; init; }
    public int Qty { get; init; }
}