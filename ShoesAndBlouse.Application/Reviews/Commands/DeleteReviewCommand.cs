using MediatR;

namespace ShoesAndBlouse.Application.Reviews.Commands
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteReviewCommand(int id)
        {
            Id = id;
        }
    }
}