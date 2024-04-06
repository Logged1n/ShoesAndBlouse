using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        //Find the product in repository
        var productToDelete = await _productRepository.GetProductById(request.productId, cancellationToken);

        if (productToDelete is null)
            return false; // product does not exist, so delete failed
        
        return await _productRepository.DeleteProduct(productToDelete.Id, cancellationToken); // if it was found try to Delete it
    }
}