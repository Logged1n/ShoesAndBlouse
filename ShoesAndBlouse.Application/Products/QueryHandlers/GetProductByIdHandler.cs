using MediatR;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.QueryHandlers;

public class GetProductByIdHandler(IProductRepository productRepository) : IRequestHandler<GetProductById, Product>
{
    public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        return await productRepository.GetProductById(request.Id, cancellationToken);
    }
}