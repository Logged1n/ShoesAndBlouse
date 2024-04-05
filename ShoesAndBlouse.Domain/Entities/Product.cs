using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; } = new("zl", 299.99m);
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public string PhotoPath { get; set; } = string.Empty;
}