using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductById(request.ProductId, cancellationToken);

            if (existingProduct is null)
                return false; // Product does not exist - return false, there is nothing to update

            // Update Product data
            if (request.Name is not null)
                existingProduct.Name = request.Name;

            if (request.Description is not null)
                existingProduct.Description = request.Description;

            if (request.Price is not null)
                existingProduct.Price = request.Price;

            if (request.Category is not null)
                existingProduct.Categories = await _categoryRepository.GetCategoriesByNames(request.Category, cancellationToken); // get the categories from categoryRepository

            if (request.PhotoPath is not null)
                existingProduct.PhotoPath = request.PhotoPath;

            await _productRepository.UpdateProduct(existingProduct, cancellationToken); // Update it in repository

            return true; // Update succeeded!
        }
    }
}