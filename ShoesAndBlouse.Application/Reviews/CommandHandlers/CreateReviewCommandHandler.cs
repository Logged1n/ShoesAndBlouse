using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Reviews.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Reviews.CommandHandlers;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository, IProductRepository productRepository, IUserRepository userRepository)
    {
        _reviewRepository = reviewRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);
        var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (product == null || user == null)
        {
            throw new ArgumentNullException();
        }

        var review = new Review
        {
            Score = request.Score,
            Product = product,
            User = user,
            Title = request.Title,
            Description = request.Description
        };

        await _reviewRepository.CreateReviewAsync(review, cancellationToken);
        return ReviewMapper.MapToDto(review);
    }
}