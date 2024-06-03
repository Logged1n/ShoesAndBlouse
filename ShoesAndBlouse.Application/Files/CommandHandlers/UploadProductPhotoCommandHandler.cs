using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using ShoesAndBlouse.Application.Files.Commands;
using ShoesAndBlouse.Domain.Interfaces;


namespace ShoesAndBlouse.Application.Files.CommandHandlers
{
    public class UploadPhotoCommandHandler : IRequestHandler<UploadProductPhotoCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IHostEnvironment _webHostEnvironment;

        public UploadPhotoCommandHandler(IProductRepository productRepository, IHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Handle(UploadProductPhotoCommand request, CancellationToken cancellationToken)
        {
            if (request.PhotoFile == null || request.PhotoFile.Length == 0)
                throw new ArgumentException("Photo file is empty.");

            // Save photo to local storage
            var photoPath = await SavePhotoAsync(request.PhotoFile, request.ProductId);

            // Update product record with photo path
            var productToUpdate = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);
            if (productToUpdate is not null)
            {
                productToUpdate.PhotoPath = photoPath;
                await _productRepository.UpdateProductAsync(productToUpdate, cancellationToken);
            }
        }

        async private Task<string> SavePhotoAsync(IFormFile photoFile, int productId)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images/Product");


            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{productId}{Path.GetExtension(photoFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(fileStream);
            }

            return Path.Combine("Images/Product", fileName);
        }
            
    }
}
