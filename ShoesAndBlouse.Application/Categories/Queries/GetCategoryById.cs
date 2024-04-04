using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetCategoryById : IRequest<Category?>
{
    public int categoryId { get; set; }
}