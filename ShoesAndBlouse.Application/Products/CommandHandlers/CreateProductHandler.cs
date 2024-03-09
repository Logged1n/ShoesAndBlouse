using MediatR;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities; 

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class CreateProductHandler(IProductRepository productRepository) : IRequestHandler<CreateProduct, Domain.Entities.Product>
{
    public async Task<Domain.Entities.Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await productRepository.CreateProduct(product);
    }
}