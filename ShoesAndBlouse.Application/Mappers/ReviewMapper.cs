using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers;

public static class ReviewMapper
{
    public static ReviewDto MapToDto(Review review)
    {
        return new ReviewDto
        {
            Id = review.Id.ToString(),
            Score = review.Score,
            ProductId = review.Product.Id.ToString(),
            UserId = review.User.Id.ToString(),
            Title = review.Title,
            Description = review.Description
        };
    }

    public static List<ReviewDto> MapListToDto(ICollection<Review> reviews)
    {
        return reviews.Select(MapToDto).ToList();
    }
}