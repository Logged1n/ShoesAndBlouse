using MediatR;

namespace ShoesAndBlouse.Application.Reviews.Commands
{
    public class UpdateReviewCommand : IRequest<bool>
    {
        public int ReviewId { get; set; }
        public int? Score { get; set; } 
        public string? Title { get; set; } 
        public string? Description { get; set; } 
    }
}