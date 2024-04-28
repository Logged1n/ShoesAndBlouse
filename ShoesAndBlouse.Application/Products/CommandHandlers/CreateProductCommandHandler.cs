using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _environment;

        public CreateProductCommandHandler(IProductRepository productRepository,
            ICategoryRepository categoryRepository, IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _environment = environment;
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

            await _productRepository.CreateProduct(product, cancellationToken);
            return ProductMapper.MapToDto(product);
        }
    }
}
