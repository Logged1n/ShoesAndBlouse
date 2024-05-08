using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CreateProductCommandHandler(IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Categories = []
            };

            foreach (var categoryId in request.CategoryIds)
            {
                var category = await _categoryRepository.GetCategoryById(categoryId,
                    cancellationToken);
                if (category is not null)
                    product.Categories.Add(category);
            }

            product.PhotoPath = $"Images/Product/0";
            await _productRepository.CreateProductAsync(product, cancellationToken);
            return ProductMapper.MapToDto(product);
        }
    }
}
