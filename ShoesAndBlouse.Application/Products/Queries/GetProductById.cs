using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetProductById : IRequest<Product?>
{
    public int Id { get; set; }
}