using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Product
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; } = new("PLN", 299.99m);
    public List<Category> Categories { get; init; } = [];
    public List<Review> Reviews { get; init; } = [];
    public string PhotoPath { get; set; } = string.Empty;
}