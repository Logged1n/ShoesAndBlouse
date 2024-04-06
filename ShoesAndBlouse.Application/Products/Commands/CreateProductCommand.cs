using MediatR;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Products.Commands;

public record CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; } = new("zl", 299.99m);
    public List<string> CategoryNames {get; set;} = [];
    public string PhotoPath {get; set;} = string.Empty;
}
