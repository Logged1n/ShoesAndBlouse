using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Reviews.Commands;

public record CreateReviewCommand : IRequest<ReviewDto>
{
    public int Score { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}