using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class CreateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<CreateProduct, Product>
{
    public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetCategoriesByNames(request.CategoryNames, cancellationToken);
        
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = categories,
            PhotoPath = request.PhotoPath,
        };

        return await productRepository.CreateProduct(product, cancellationToken);
    }
}
