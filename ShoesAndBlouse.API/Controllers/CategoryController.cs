using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShoesAndBlouse.API.Validators.Category;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetById/{categoryId}")]
    public async Task<IActionResult> GetById(int categoryId)
    {
        return Ok(await _mediator.Send(new GetCategoryByIdQuery { categoryId = categoryId }));
    }

    [HttpGet("GetByName/{categoryName}")]
    public async Task<IActionResult> GetByName(string categoryName)
    {
        return Ok(await _mediator.Send(new GetCategoryByNameQuery { Name = categoryName }));
    }
    
    [HttpGet("GetByNames")]
    public async Task<IActionResult> GetByNames([FromBody] List<string> categoriesNames)
    {
        return Ok(await _mediator.Send(new GetCategoriesByNamesQuery { categoryNames = categoriesNames}));
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
        return CreatedAtAction(nameof(GetById), new { Id = category.Id }, category);
    }

    [HttpDelete("Delete/{categoryId}")]
    public async Task<IActionResult> Delete(int categoryId)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(categoryId));

        if (result) return Ok();
        return NotFound();
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