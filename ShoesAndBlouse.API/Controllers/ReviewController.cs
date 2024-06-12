using System.Security.Claims;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Reviews.Commands;
using ShoesAndBlouse.Application.Reviews.Queries;

namespace ShoesAndBlouse.API.Controllers;

[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[ApiVersion(1)]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    public ReviewController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [MapToApiVersion(1)]
    [HttpGet("GetById/{reviewId}")]
    public async Task<ActionResult<ReviewDto>> GetById(int reviewId)
    {
        var review = await _mediator.Send(new GetReviewByIdQuery { Id = reviewId });
        if (Convert.ToInt32(reviewId) == 0)
            return NotFound(review);

        return Ok(review);
    }
    
    [MapToApiVersion(1)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ReviewDto>>> GetAll()
    {
        var reviews = await _mediator.Send(new GetAllReviewsQuery());
        return Ok(reviews);
    }

    [MapToApiVersion(1)]
    [HttpGet("GetAllProduct/{productId}")]
    public async Task<ActionResult<List<ReviewDto>>> GetAllProductReviews(int productId)
    {
        var query = new GetAllProductReviewsQuery { ProductId = productId };
        var reviews = await _mediator.Send(query);
        return Ok(reviews);
    }
    
    [MapToApiVersion(1)]
    [Authorize(Roles = "Client, Manager, Admin")]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateReviewCommand command)
    {
        var review = await _mediator.Send(command);
        return Ok(review);
    }

    [MapToApiVersion(1)]
    [Authorize(Roles = "Admin, Manager, Client")]
    [HttpDelete("Delete/{reviewId}")]
    public async Task<IActionResult> Delete(int reviewId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reviewToDelete = await GetById(reviewId);
        if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
        {
            if (userId != reviewToDelete.Value.UserId) 
                return Unauthorized();
        }
        await _mediator.Send(new DeleteReviewCommand(reviewId));
        return Ok();
    }
    
    [MapToApiVersion(1)]
    [Authorize(Roles = "Admin, Manager, Client")]
    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UpdateReviewCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reviewToUpdate = await GetById(command.ReviewId);
        if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
        {
            if (userId != reviewToUpdate.Value.UserId) 
                return Unauthorized();
        }
        
        var result = await _mediator.Send(command);

        if (result)
            return Ok();
        else
            return NotFound();
    }
}