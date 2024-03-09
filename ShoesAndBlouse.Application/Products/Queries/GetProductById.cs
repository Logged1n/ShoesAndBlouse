using MediatR;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetProductById : IRequest<Domain.Entities.Product>
{
    public int Id { get; set; }
    
}