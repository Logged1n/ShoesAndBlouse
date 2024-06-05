using Asp.Versioning;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Application.Validators.Category;

namespace ShoesAndBlouse.API.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [MapToApiVersion(1)]
    [HttpGet("GetById/{categoryId}")]
    public async Task<IActionResult> GetById(int categoryId)
    {
        return Ok(await _mediator.Send(new GetCategoryByIdQuery { Id = categoryId }));
    }
    
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        ValidationResult validationResult = await new CreateCategoryCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var category = await _mediator.Send(command);
        return Ok(category);
    }

    [HttpDelete("Delete/{categoryId}")]
    public async Task<IActionResult> Delete(int categoryId)
    {
        await _mediator.Send(new DeleteCategoryCommand(categoryId));

        return Ok();
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
    {
        var validationResult = await new UpdateCategoryCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var result = await _mediator.Send(command);

        if (result)
            return Ok();
        else
            return NotFound();
    }
}