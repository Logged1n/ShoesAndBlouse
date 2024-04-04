using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IReviewRepository
{
    Task<ICollection<Review>> GetAll(CancellationToken cancellationToken=default);
    Task<Review?> GetReviewById(int reviewId, CancellationToken cancellationToken=default);
    Task<Review> CreateReview(Review toCreate, CancellationToken cancellationToken=default);
    Task<Review?> UpdateReview(Review toUpdate, CancellationToken cancellationToken=default);
    Task<bool> DeleteReview(int reviewId, CancellationToken cancellationToken=default);
}