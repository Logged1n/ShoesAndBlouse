using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Reviews.Queries;

public record GetAllProductReviewsQuery : IRequest<List<ReviewDto>>
{
    public int ProductId { get; set; }
}