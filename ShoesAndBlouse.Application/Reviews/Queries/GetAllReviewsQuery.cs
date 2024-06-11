using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Reviews.Queries
{
    public class GetAllReviewsQuery : IRequest<List<ReviewDto>> { }
}