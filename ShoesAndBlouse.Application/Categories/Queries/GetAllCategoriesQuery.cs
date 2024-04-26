using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;