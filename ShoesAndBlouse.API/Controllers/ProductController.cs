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

    [HttpDelete("DeleteProduct/{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _mediator.Send(new DeleteProduct(productId));

        if (result) return NoContent();
        return NotFound();
    }

    [HttpPatch("UpdateProduct/{productId}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProduct command)
    {
        if (productId != command.ProductId)
        {
            return BadRequest(); // Zwracamy BadRequest (400), jeśli identyfikator produktu w ścieżce URL nie zgadza się z identyfikatorem w ciele żądania.
        }

        var result = await _mediator.Send(command);

        if (result)
            return NoContent(); // Zwracamy NoContent (204) jeśli produkt został pomyślnie zaktualizowany.
        else
            return NotFound(); // Zwracamy NotFound (404) jeśli produkt o podanym identyfikatorze nie został znaleziony.

    }
    
}