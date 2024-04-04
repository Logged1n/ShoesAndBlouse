using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductById(request.ProductId, cancellationToken);

            if (existingProduct is null)
                return false; // Produkt nie istnieje, zwracamy false.

            // Aktualizacja danych produktu na podstawie danych przekazanych w UpdateProductCommand.
            if (request.Name is not null)
                existingProduct.Name = request.Name;

            if (request.Description is not null)
                existingProduct.Description = request.Description;

            if (request.Price is not null)
                existingProduct.Price = request.Price;

            if (request.Category is not null)
                existingProduct.Category = request.Category;

            if (request.PhotoPath is not null)
                existingProduct.PhotoPath = request.PhotoPath;

            await _productRepository.UpdateProduct(existingProduct, cancellationToken); // Aktualizacja produktu w repozytorium.

            return true; // Zwracamy true, jeśli produkt został pomyślnie zaktualizowany.
        }
    }
}