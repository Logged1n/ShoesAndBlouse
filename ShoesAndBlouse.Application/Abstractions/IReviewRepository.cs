using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Abstractions;

public interface IReviewRepository
{
    Task<ICollection<Domain.Entities.Review>> GetAll();
    Task<Domain.Entities.Review?> GetReviewById(int reviewId);
    Task<Domain.Entities.Review> CreateReview(Domain.Entities.Review toCreate);
    Task<Domain.Entities.Review?> UpdateReview(int reviewId, int score, int productId, int userId, string title, string description);
    Task<Domain.Entities.Review?> DeleteReview(int reviewId);
}