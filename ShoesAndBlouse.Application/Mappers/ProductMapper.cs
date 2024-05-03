using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto MapToDto(Product? product)
        {
            if (product is not null)
            {
                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Categories = product.Categories.ToDictionary(c => c.Id, c => c.Name),
                    PhotoUrl = product.PhotoPath
                };
            }

            throw new Exception("Product is null!");
        }

        public static List<ProductDto> MapToDtoList(IEnumerable<Product> products)
        {
            return products.Select(MapToDto).ToList();
        }
    }
}