using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

    public class ReviewRepository(PostgresDbContext context) : IReviewRepository
    {
        public async Task<ICollection<Review>> GetAll(CancellationToken cancellationToken=default)
        {
            return await context.Review.ToListAsync(cancellationToken);
        }

        public async Task<Review?> GetReviewById(int reviewId, CancellationToken cancellationToken)
        {
            return await context.Review.FirstOrDefaultAsync(r => r.Id == reviewId, cancellationToken);
        }

        public async Task<Review> CreateReview(Review toCreate, CancellationToken cancellationToken=default)
        {
            context.Review.Add(toCreate);
            await context.SaveChangesAsync(cancellationToken);
            return toCreate;
        }
        

        public async Task<Review?> UpdateReview(Review toUpdate, CancellationToken cancellationToken=default)
        {
            var review = await context.Review.FirstOrDefaultAsync(r => r.Id == toUpdate.Id, cancellationToken);
            if (review is null) 
                return await CreateReview(toUpdate, cancellationToken);

            review.Title = toUpdate.Title;
            review.Score = toUpdate.Score;
            review.ProductId = toUpdate.ProductId;
            review.UserId = toUpdate.UserId;
            review.Description = toUpdate.Description;
            await context.SaveChangesAsync(cancellationToken);
        
            return review;
        }

        public async Task<Review?> DeleteReview(int reviewId, CancellationToken cancellationToken=default)
        {
            var review = context.Review.FirstOrDefault(r => r.Id == reviewId);

            if (review is null) return review;
        
            context.Review.Remove(review);

            await context.SaveChangesAsync(cancellationToken);
            return review;
        }
    }
