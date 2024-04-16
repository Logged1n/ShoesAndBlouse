using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<Category>
{
    public string Name { get; set; } = string.Empty;
    public List<int> Products { get; set; } = [];
}