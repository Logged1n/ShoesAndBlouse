using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.Files.Commands;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[ApiVersion(1)]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize(Roles = "Admin")]
    [MapToApiVersion(1)]
    [HttpPut("UploadProductImage/{productId}")]
    public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
    {
        var command = new UploadProductPhotoCommand { ProductId = productId, PhotoFile = file };
        await _mediator.Send(command);
        return Ok();
    }
}