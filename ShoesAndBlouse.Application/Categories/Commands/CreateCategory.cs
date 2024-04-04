using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.Commands;

public record CreateCategory : IRequest<Category>
{
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];
}