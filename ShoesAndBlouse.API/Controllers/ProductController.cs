using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Products.Queries;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetProductById/{productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        return Ok(await _mediator.Send(new GetProductById { Id = productId}));
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _mediator.Send(new GetAllProducts());
        return Ok(products);
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProduct command)
    {
        await _mediator.Send(command);
        return Created();
    }
    
}