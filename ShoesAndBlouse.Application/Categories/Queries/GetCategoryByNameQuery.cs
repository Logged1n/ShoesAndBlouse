using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetCategoryByNameQuery : IRequest<Category?>
{
    public string Name { get; set; } = string.Empty;
}