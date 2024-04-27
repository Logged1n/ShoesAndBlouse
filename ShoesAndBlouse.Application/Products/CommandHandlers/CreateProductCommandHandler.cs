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

        private string GetPhotoPath(int productId)
        {
            // Generuj unikalną nazwę pliku na podstawie ID produktu i oryginalnej nazwy pliku
            var uniqueFileName = $"{productId}.png";

            // Pobierz ścieżkę do folderu wwwroot/Images/Product, gdzie chcemy zapisać zdjęcie
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "Images", "Product");

            // Utwórz folder uploads, jeśli nie istnieje
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Połącz ścieżkę folderu uploads z unikalną nazwą pliku
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            return filePath;
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

            // Zapisz przesłane zdjęcie lokalnie
            if (request.Photo != null)
            {
                var photoPath = GetPhotoPath(product.Id);
                await using(var stream = new FileStream(photoPath, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(stream, cancellationToken);
                }
                product.PhotoPath = photoPath;
            }

            await _productRepository.CreateProduct(product, cancellationToken);
            return ProductMapper.MapToDto(product);
        }
    }
}
