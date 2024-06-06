namespace ShoesAndBlouse.Application.DTOs;

public class CategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Dictionary<int, string> Products { get; set; } = [];
}