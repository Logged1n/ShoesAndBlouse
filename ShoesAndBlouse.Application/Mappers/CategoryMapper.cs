using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Products = category.Products.ToDictionary(p => p.Id, p => p.Name)
        };
    }
    public static List<CategoryDto> MapListToDto(ICollection<Category> categories)
    {
        return categories.Select(MapToDto).ToList();
    }
}