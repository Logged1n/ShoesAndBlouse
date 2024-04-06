using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetProductByIdQuery : IRequest<Product?>
{
    public int Id { get; set; }
}