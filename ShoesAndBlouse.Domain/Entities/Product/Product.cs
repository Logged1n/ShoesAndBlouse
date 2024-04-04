using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities.Product;

public sealed class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; } = new("zl", 299.99m);
    public List<Category> Categories { get; set; } = [];
    public string PhotoPath { get; set; } = string.Empty;
}