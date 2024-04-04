using MediatR;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class CreateProductHandler(IProductRepository productRepository) : IRequestHandler<CreateProduct, Product>
{
    public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Categories,
            PhotoPath = request.PhotoPath,
        };

        return await productRepository.CreateProduct(product, cancellationToken);
    }
}
