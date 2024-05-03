using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesAndBlouse.Application.Files.Commands;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion(1)]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    //[Authorize("Admin")]
    [HttpPut("UploadProductImage")]
    public async Task<IActionResult> UploadProductImage(UploadProductPhotoCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}