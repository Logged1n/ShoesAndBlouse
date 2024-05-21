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
        var existingCategory = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
        
        if (existingCategory is null)
            return false;

        if (request.Name is not null)
            existingCategory.Name = request.Name;
        if (request.ProductsIds is not null)
        {
            existingCategory.Products.Clear();
            foreach (var productId in request.ProductsIds)
            {
                var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
                if (product is not null)
                {
                    existingCategory.Products.Add(product);
                    product.Categories.Add(existingCategory);
                }
            }
        }

        await _categoryRepository.UpdateCategoryAsync(existingCategory, cancellationToken);

        return true;
    }
}