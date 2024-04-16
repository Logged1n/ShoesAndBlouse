using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository) : IRequestHandler<CreateCategoryCommand, Category>
{
    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Products = new List<Product>(),
        };
        foreach (var productId in request.Products)
        {
            var product = await productRepository.GetProductById(productId, cancellationToken);
            if (product != null)
            {
                category.Products.Add(product);
            }
        }
        return await categoryRepository.CreateCategory(category, cancellationToken);
    }
}