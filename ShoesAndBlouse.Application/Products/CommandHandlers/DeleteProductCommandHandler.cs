using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Znajdź produkt w repozytorium
            var productToDelete = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (productToDelete is null)
                return; // Produkt nie istnieje, więc nie można go usunąć

            // Usuń powiązane zdjęcie produktu
            var photoPath = productToDelete.PhotoPath;
            if (!string.IsNullOrEmpty(photoPath))
            {
                // Usuń plik zdjęcia
                File.Delete(photoPath);
            }

            // Usuń produkt z repozytorium
            await _productRepository.DeleteProductAsync(productToDelete.Id, cancellationToken);
        }
    }
}