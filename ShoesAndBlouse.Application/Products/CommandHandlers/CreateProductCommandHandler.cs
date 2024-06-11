using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers
{
    public class CreateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
        : IRequestHandler<CreateProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Categories = [],
                Reviews = [],
                PhotoPath = string.Empty
            };

            foreach (var categoryId in request.CategoryIds)
            {
                var category = await categoryRepository.GetCategoryByIdAsync(categoryId,
                    cancellationToken);
                if (category is not null)
                    product.Categories.Add(category);
            }

            product.PhotoPath = "Images/Product/0.png";
            await productRepository.CreateProductAsync(product, cancellationToken);
            return ProductMapper.MapToDto(product);
        }
    }
}
