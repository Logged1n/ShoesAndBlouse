using Asp.Versioning;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Application.Validators.Product;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[ApiVersion(1)]
//[Authorize("Admin")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    static private string _hostUrl = string.Empty;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [HttpGet("GetById/{productId}")]
    public async Task<ActionResult<ProductDto>> GetById(int productId)
    {
        var product = await _mediator.Send(new GetProductByIdQuery { Id = productId });
        if (product is null)
            return NotFound();

        _hostUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        product.PhotoUrl = $"{_hostUrl}/{product.PhotoUrl}";
        return Ok(product);
    }
    
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        _hostUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        var products = await _mediator.Send(new GetAllProductsQuery());
        foreach (var product in products)
        {
            product.PhotoUrl = $"{_hostUrl}/{product.PhotoUrl}";
        }
        return Ok(products);
    }
    
    [MapToApiVersion(1)]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        ValidationResult validationResult = await new CreateProductCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        _hostUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        var product = await _mediator.Send(command);
        product.PhotoUrl =$"{_hostUrl}/{product.PhotoUrl}";
        
        return Ok(product);
    }

    [MapToApiVersion(1)]
    [HttpDelete("Delete/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await _mediator.Send(new DeleteProductCommand(productId));

        return Ok();
    }
    
    [MapToApiVersion(1)]
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