using MediatR;
using Microsoft.AspNetCore.Hosting;
using ShoesAndBlouse.Application.Files.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Files.CommandHandlers;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IWebHostEnvironment _environment;

    public UploadFileCommandHandler(IProductRepository productRepository, IWebHostEnvironment environment)
    {
        _productRepository = productRepository;
        _environment = environment;
    }
    private string GetProductPhotoPath(int productId)
    {
        // Generuj unikalną nazwę pliku na podstawie ID produktu i oryginalnej nazwy pliku
        var uniqueFileName = $"{productId}.png";

        // Pobierz ścieżkę do folderu wwwroot/Images/Product, gdzie chcemy zapisać zdjęcie
        var uploadsFolder = Path.Combine("Images", "Product");

        // Utwórz folder uploads, jeśli nie istnieje
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Połącz ścieżkę folderu uploads z unikalną nazwą pliku
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        return filePath;
    }
    
    public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        ;// Zapisz przesłane zdjęcie lokalnie
        if (request.File is not null)
        {
            var photoPath = GetProductPhotoPath(_productRepository.GetNextProductId());
            await using(var stream = new FileStream(Path.Combine(_environment.WebRootPath, photoPath), FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }
            request.Product.PhotoPath = photoPath;
        }
    }
}