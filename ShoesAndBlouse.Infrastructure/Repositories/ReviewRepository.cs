using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

    public class ReviewRepository(PostgresDbContext context) : IReviewRepository
    {
        public async Task<ICollection<Review>> GetAll()
        {
            return await context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReviewById(int reviewId)
        {
            return await context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task<Review> CreateReview(Review toCreate, CancellationToken cancellationToken=default)
        {
            context.Reviews.Add(toCreate);
            await context.SaveChangesAsync();
            return toCreate;
        }
        

        public async Task<Review?> UpdateReview(Review toUpdate, CancellationToken cancellationToken=default)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == toUpdate.Id, cancellationToken);
            if (review is null) return review;

            review.Title = toUpdate.Title;
            review.Score = toUpdate.Score;
            review.Product = toUpdate.Product;
            review.User = toUpdate.User;
            review.Description = toUpdate.Description;
            await context.SaveChangesAsync(cancellationToken);
        
            return review;
        }

        public async Task<Review?> DeleteReview(int reviewId, CancellationToken cancellationToken=default)
        {
            var review = context.Reviews
                .FirstOrDefault(r => r.Id == reviewId);

            if (review is null) return null;
        
            context.Reviews.Remove(review);

            await context.SaveChangesAsync(cancellationToken);
            return review;
        }
    }
