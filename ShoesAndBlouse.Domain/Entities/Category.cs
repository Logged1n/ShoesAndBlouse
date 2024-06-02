using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Category
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; init; } = [];

}