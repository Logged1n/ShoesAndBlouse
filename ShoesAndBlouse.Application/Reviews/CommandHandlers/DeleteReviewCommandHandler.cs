using MediatR;
using ShoesAndBlouse.Application.Reviews.Commands;
using ShoesAndBlouse.Domain.Interfaces;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
{
    private readonly IReviewRepository _reviewRepository;

    public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewRepository.DeleteReviewAsync(request.Id, cancellationToken);
    }
}