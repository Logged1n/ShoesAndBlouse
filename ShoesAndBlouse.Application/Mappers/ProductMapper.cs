using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers;

public static class ProductMapper
{
    public static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Categories = product.Categories.ToDictionary(c => c.Id, c => c.Name)
            //Photo = ? //TODO
        };
    }

    public static List<ProductDto> MapListToDto(List<Product> products)
    {
        return products.Select(MapToDto).ToList();
    }
}