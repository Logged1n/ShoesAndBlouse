using MediatR;
using ShoesAndBlouse.Application.Reviews.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Application.DTOs;

public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetAllReviewsQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<List<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository.GetAllAsync(cancellationToken);
        return reviews.Select(review => new ReviewDto
        {
            Id = review.Id.ToString(),
            Score = review.Score,
            ProductId = review.Product,
            UserId = review.User,
            Title = review.Title,
            Description = review.Description,
            // Map other properties
        }).ToList();
    }
}