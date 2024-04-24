using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];

}