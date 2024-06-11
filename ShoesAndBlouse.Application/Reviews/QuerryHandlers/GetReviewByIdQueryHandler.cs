using MediatR;
using ShoesAndBlouse.Application.Reviews.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Application.DTOs;

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto>
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ReviewDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(request.Id, cancellationToken);
        if (review == null)
        {
            return null;
        }

        return new ReviewDto
        {
            Id = review.Id.ToString(),
            Score = review.Score,
            ProductId = review.Product,
            UserId = review.User,
            Title = review.Title,
            Description = review.Description,
            // Map other properties
        };
    }
}
