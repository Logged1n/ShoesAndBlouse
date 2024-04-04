using MediatR;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.Commands;

public sealed record CreateProduct : IRequest<Product>
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Money? Price { get; set; }
    public List<Category> Categories {get; set;} = [];
    public string PhotoPath {get; set;} = string.Empty;
}
