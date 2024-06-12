using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Reviews.Queries;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Reviews.QueryHandlers;

public class GetAllProductReviewsQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetAllProductReviewsQuery, List<ReviewDto>>
{

    public async Task<List<ReviewDto>> Handle(GetAllProductReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await reviewRepository.GetAllProductReviewsAsync(request.ProductId, cancellationToken);
        
        return reviews.Select(review => new ReviewDto
        {
           Id = review.Id.ToString(),
           Score = review.Score,
           ProductId = review.Product.Id.ToString(),
           UserId = review.User.Id.ToString(),
           Title = review.Title,
           Description = review.Description,
        }).ToList();
    }
}