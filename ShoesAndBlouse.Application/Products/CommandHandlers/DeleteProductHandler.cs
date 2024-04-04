using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class DeleteProductHandler : IRequestHandler<DeleteProduct, bool>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<bool> Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        var productToDelete = await _productRepository.GetProductById(request.productId, cancellationToken);

        if (productToDelete is null)
            return false; // product does not exist

        await _productRepository.DeleteProduct(productToDelete.Id, cancellationToken);

        return true;
    }
}