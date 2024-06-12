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
                List<int> reviewIds = [];
                foreach (var r in product.Reviews)
                {
                    reviewIds.Add(r.Id);
                }
                
                return new ProductDto
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Categories = product.Categories.ToDictionary(c => c.Id, c => c.Name),
                    ReviewIds = reviewIds ,
                    PhotoUrl = product.PhotoPath
                };
            }

            throw new Exception("Product is null!");
        }

        public static List<ProductDto> MapToDtoList(ICollection<Product> products)
        {
            return products.Select(MapToDto).ToList();
        }
    }
}