using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesAndBlouse.API.Validators.Product;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Entities;

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

    [HttpGet("GetById/{productId}")]
    public async Task<IActionResult> GetById(int productId)
    {
        return Ok(await _mediator.Send(new GetProductByIdQuery { Id = productId}));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        ValidationResult validationResult = await new CreateProductCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var product = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id= product.Id}, product);
    }

    [HttpDelete("Delete/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        var result = await _mediator.Send(new DeleteProductCommand(productId));

        if (result) return Ok();
        return NotFound();
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
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