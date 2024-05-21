using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IReviewRepository
{
    Task<ICollection<Review>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<Review?> GetReviewByIdAsync(int reviewId, CancellationToken cancellationToken=default);
    Task<Review> CreateReviewAsync(Review toCreate, CancellationToken cancellationToken=default);
    Task<Review?> UpdateReviewAsync(Review toUpdate, CancellationToken cancellationToken=default);
    Task<bool> DeleteReviewAsync(int reviewId, CancellationToken cancellationToken=default);
}