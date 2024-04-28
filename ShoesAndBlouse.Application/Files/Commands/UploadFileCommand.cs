using MediatR;
using Microsoft.AspNetCore.Http;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Files.Commands;

public record UploadFileCommand : IRequest
{
    public Product Product { get; set; } 
    public IFormFile? File { get; set; }
}