using MediatR;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.Commands;

public sealed record CreateProduct : IRequest<Product>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Money? Price { get; set; }
}
