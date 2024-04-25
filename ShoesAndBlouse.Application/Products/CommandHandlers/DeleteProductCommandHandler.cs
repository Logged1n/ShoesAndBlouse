using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        //Find the product in repository
        var productToDelete = await _productRepository.GetProductById(request.productId, cancellationToken);

        if (productToDelete is null)
            return; // product does not exist, so delete failed
        
        await _productRepository.DeleteProduct(productToDelete.Id, cancellationToken); // if it was found try to Delete it
    }
}