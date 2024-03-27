using MediatR;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.QueryHandlers;

public class GetAllProductsHandler(IProductRepository productRepository) : IRequestHandler<GetAllProducts, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetAllProducts request, CancellationToken cancellationToken)
    {
        return await productRepository.GetAll(cancellationToken);
    }
}