using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Products = [],
        };
        foreach (var productId in request.ProductsIds)
        {
            var product = await productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (product is not null)
                category.Products.Add(product);
        }
        
        await categoryRepository.CreateCategoryAsync(category, cancellationToken);
        return CategoryMapper.MapToDto(category);
    }
}