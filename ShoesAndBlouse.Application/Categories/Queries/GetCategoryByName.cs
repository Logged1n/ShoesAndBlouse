using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetCategoryByName : IRequest<Category?>
{
    public string Name { get; set; } = string.Empty;
}