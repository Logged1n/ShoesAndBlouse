using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetCategoryById(request.CategoryId, cancellationToken);
        
        if (existingCategory is null)
            return false;

        var category = new Category
        {
            Name = request.Name,
            Products = new List<Product>(),
        };
        if (category.Name is not null)
            existingCategory.Name = category.Name;
        
        foreach (var productId in category.Products)
        {
            var product = await _productRepository.GetProductById(Convert.ToInt32(productId), cancellationToken);
            if (product != null)
            {
                category.Products.Add(product);
            }
        }
        if (category.Products is not null)
            existingCategory.Products = category.Products;

        await _categoryRepository.UpdateCategory(existingCategory, cancellationToken);

        return true;
    }
}