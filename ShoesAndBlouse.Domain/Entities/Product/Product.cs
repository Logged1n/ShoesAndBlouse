namespace ShoesAndBlouse.Domain.Entities.Product;

public sealed class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get;  set; }
    //public Category? Category { get; set; }
    //public string PhotoPath { get; set; } = string.Empty;
};