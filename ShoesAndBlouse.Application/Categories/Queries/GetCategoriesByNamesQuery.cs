using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetCategoriesByNamesQuery : IRequest<ICollection<Category>>
{
    public List<string> categoryNames = new();
}