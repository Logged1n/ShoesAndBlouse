using MediatR;
using ShoesAndBlouse.Application.Reviews.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Reviews.CommandHandlers;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, bool>
{
    private readonly IReviewRepository _reviewRepository;

    public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var existingReview = await _reviewRepository.GetReviewByIdAsync(request.Id, cancellationToken);

        if (existingReview is null)
            return false;

        if (request.Score.HasValue)
            existingReview.Score = request.Score.Value;

        if (request.Title is not null)
            existingReview.Title = request.Title;

        if (request.Description is not null)
            existingReview.Description = request.Description;

        await _reviewRepository.UpdateReviewAsync(existingReview, cancellationToken);

        return true;
    }
}