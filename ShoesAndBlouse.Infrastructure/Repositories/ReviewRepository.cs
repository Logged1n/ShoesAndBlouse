using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

    public class ReviewRepository(PostgresDbContext context) : IReviewRepository
    {
        public async Task<ICollection<Review>> GetAll()
        {
            return await context.Review.ToListAsync();
        }

        public async Task<Review?> GetReviewById(int reviewId)
        {
            return await context.Review.FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task<Review> CreateReview(Review toCreate, CancellationToken cancellationToken=default)
        {
            context.Review.Add(toCreate);
            await context.SaveChangesAsync();
            return toCreate;
        }
        

        public async Task<Review?> UpdateReview(Review toUpdate, CancellationToken cancellationToken=default)
        {
            var review = await context.Review.FirstOrDefaultAsync(r => r.Id == toUpdate.Id);
            if (review is null) return review;

            review.Title = toUpdate.Title;
            review.Score = toUpdate.Score;
            review.ProductId = toUpdate.ProductId;
            review.UserId = toUpdate.UserId;
            review.Description = toUpdate.Description;
            await context.SaveChangesAsync();
        
            return review;
        }

        public async Task<Review?> DeleteReview(int reviewId, CancellationToken cancellationToken=default)
        {
            var review = context.Review
                .FirstOrDefault(r => r.Id == reviewId);

            if (review is null) return null;
        
            context.Review.Remove(review);

            await context.SaveChangesAsync();
            return review;
        }
    }
