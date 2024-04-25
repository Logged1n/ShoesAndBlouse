using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

            if (request.CategoryIds is not null)
            {
                existingProduct.Categories.Clear();
                foreach (var categoryId in request.CategoryIds)
                {
                    var category = await _categoryRepository.GetCategoryById(categoryId, cancellationToken);
                    if (category is not null)
                    {
                        existingProduct.Categories.Add(category);
                        category.Products.Add(existingProduct);
                    }
                    
                }
            }

            if (request.PhotoPath is not null)
                existingProduct.PhotoPath = request.PhotoPath;

            await _productRepository.UpdateProduct(existingProduct, cancellationToken); // Update it in repository

            return true; // Update succeeded!
        }
    }
}