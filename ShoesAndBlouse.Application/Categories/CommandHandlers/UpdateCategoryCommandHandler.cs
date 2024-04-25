using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
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

        if (request.Name is not null)
            existingCategory.Name = request.Name;
        if (request.Products is not null)
        {
            List<Product> newProducts = [];
            foreach (var prodId in request.Products)
            {
                var product = await _productRepository.GetProductById(prodId, cancellationToken);
                if (product is not null)
                {
                    product.Categories.Add(existingCategory);
                    newProducts.Add(product);
                }
            }
            existingCategory.Products = newProducts;
            return true;
        }
        return false;
    }
}