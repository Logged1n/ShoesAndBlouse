using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Reviews.Queries
{
    public class GetReviewByIdQuery : IRequest<ReviewDto>
    {
        public int Id { get; set; }
    }
}