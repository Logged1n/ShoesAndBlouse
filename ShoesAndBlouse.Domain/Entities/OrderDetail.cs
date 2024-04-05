using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class OrderDetail
{
    [Key]
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Qty { get; set; }
}