using MediatR;

namespace ShoesAndBlouse.Application.Reviews.Commands
{
    public class UpdateReviewCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int? Score { get; set; } // Typ zmieniony na nullable
        public string? Title { get; set; } // Typ zmieniony na nullable
        public string? Description { get; set; } // Typ zmieniony na nullable
    }
}