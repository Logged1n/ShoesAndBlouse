using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            PhotoPath = request.PhotoPath
        };

        foreach (var categoryId in request.CategoryIds)
        {
            var category = await categoryRepository.GetCategoryById(categoryId, cancellationToken);
            if (category is not null)
            {
                product.Categories.Add(category);
                category.Products.Add(product);
                await categoryRepository.UpdateCategory(category, cancellationToken);
            }
                
        }

        await productRepository.CreateProduct(product, cancellationToken);
        return product;
    }
}
