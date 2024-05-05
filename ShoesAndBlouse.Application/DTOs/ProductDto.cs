using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Money Price { get; set; }
    public Dictionary<int, string> Categories { get; set; }
    public string PhotoUrl { get; set; }
}