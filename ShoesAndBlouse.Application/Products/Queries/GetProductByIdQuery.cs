using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetProductByIdQuery : IRequest<ProductDto>
{
    public int Id { get; set; }
}