using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public int Id { get; set; }
}