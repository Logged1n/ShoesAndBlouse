using MediatR;
using Microsoft.AspNetCore.Hosting;
using ShoesAndBlouse.Application.Files.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Files.CommandHandlers;

public class UploadProductPhotoCommandHandler : IRequestHandler<UploadProductPhotoCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IWebHostEnvironment _environment;

    public UploadProductPhotoCommandHandler(IProductRepository productRepository,
        IWebHostEnvironment environment)
    {
        _productRepository = productRepository;
        _environment = environment;
    }
    private string GetProductPhotoPath(int productId)
    {
        // unique file name based on sent id
        var uniqueFileName = $"{productId}.png";

        // path to update file
        var uploadsFolder = Path.Combine("Images", "Product");

        // check if it exists/create it
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Combine path with file name
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //Return it
        return filePath;
    }
    
    public async Task Handle(UploadProductPhotoCommand request, CancellationToken cancellationToken)
    {
        // Save file locally
        if (request.File is not null)
        {
            // get the path
            var photoPath = GetProductPhotoPath(request.ProductId);
            //save it
            await using var stream = new FileStream(Path.Combine(_environment.WebRootPath, photoPath), FileMode.Create);
            await request.File.CopyToAsync(stream, cancellationToken);
            //update the product record
            var productToUpdate = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);
            productToUpdate.PhotoPath = photoPath;
            await _productRepository.UpdateProductAsync(productToUpdate, cancellationToken);
        }
    }
}