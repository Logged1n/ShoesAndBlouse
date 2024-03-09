using MediatR;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Product.Queries;

namespace ShoesAndBlouse.Application.Product.QueryHandlers;

public class GetProductByIdHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductById, Domain.Entities.Product>
{
    public async Task<Domain.Entities.Product> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        return await productRepository.GetProductById(request.Id);
    }
}