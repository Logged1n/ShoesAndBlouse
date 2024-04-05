using MediatR;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Products.Commands
{
    public class UpdateProduct : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Money? Price { get; set; }
        public List<string>? Category { get; set; }
        public string? PhotoPath { get; set; }
    }
}