using MediatR;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Money? Price { get; set; }
        public List<int>? CategoryIds { get; set; }
        
    }
}