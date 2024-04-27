using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesAndBlouse.API.Validators.Product;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Products.Queries;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _environment;
    public ProductController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    [HttpGet("GetById/{productId}")]
    public async Task<ActionResult<ProductDto>> GetById(int productId)
    {
        var product = await _mediator.Send(new GetProductByIdQuery { Id = productId });
        if (product is null)
            return NotFound();
        
        return Ok(product);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        ValidationResult validationResult = await new CreateProductCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var product = await _mediator.Send(command);
       
        return Ok(product);
    }

    [HttpDelete("Delete/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await _mediator.Send(new DeleteProductCommand(productId));

        return Ok();
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateProductCommand command)
    {
        var validationResult = await new UpdateProductCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var result = await _mediator.Send(command);

        if (result)
            return Ok();
        else
            return NotFound(); // Return NotFound (404) if product was not found.
    }
    
}