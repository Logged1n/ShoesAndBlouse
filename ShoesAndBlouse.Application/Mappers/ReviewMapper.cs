using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers;

public static class ReviewMapper
{
    public static ReviewDto MapToDto(Review? review)
    {

        if (review is not null)
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
        return new ReviewDto
        {
            Id = "0",
            Score = 5,
            ProductId = "0",
            UserId = "0",
            Title = "This Review does not exist!",
            Description = "Not an actual review"
        };
    }

    public static List<ReviewDto> MapListToDto(ICollection<Review> reviews)
    {
        return reviews.Select(MapToDto).ToList();
    }
}