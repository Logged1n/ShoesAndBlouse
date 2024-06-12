using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.DTOs;

public class ProductDto
{
    public string Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; }
    public Dictionary<int, string> Categories { get; set; } = [];
    public List<int> ReviewIds { get; set; } = [];
    public string PhotoUrl { get; set; } = string.Empty;
}