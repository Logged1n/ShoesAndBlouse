using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Abstractions;

public interface IReviewRepository
{
    Task<ICollection<Domain.Entities.Review>> GetAll();
    Task<Review?> GetReviewById(int reviewId);
    Task<Review> CreateReview(Review toCreate, CancellationToken cancellationToken=default);
    Task<Review?> UpdateReview(Review toUpdate, CancellationToken cancellationToken=default);
    Task<Review?> DeleteReview(int reviewId, CancellationToken cancellationToken=default);
}