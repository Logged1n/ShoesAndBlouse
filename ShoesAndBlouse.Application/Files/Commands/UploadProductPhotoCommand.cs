using MediatR;
using Microsoft.AspNetCore.Http;
namespace ShoesAndBlouse.Application.Files.Commands;

public record UploadProductPhotoCommand : IRequest
{
    public int ProductId { get; set; }
    public IFormFile PhotoFile { get; set; }
}