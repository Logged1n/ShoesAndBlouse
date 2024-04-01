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

        public async Task<Review> CreateReview(Review toCreate)
        {
            context.Review.Add(toCreate);
            await context.SaveChangesAsync();
            return toCreate;
        }
        

        public async Task<Review?> UpdateReview(int reviewId, int score, int productId,int userId,  string title, string description)
        {
            var review = await context.Review.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review is null) return review;
        
            review.Title = title;
            review.Score = score;
            review.ProductId = productId;
            review.UserId = userId;
            review.Description = description;
            await context.SaveChangesAsync();
        
            return review;
        }

        public async Task<Review?> DeleteReview(int reviewId)
        {
            var review = context.Review
                .FirstOrDefault(r => r.Id == reviewId);

            if (review is null) return null;
        
            context.Review.Remove(review);

            await context.SaveChangesAsync();
            return review;
        }
    }
