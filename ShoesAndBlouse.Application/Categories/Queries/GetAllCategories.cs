using MediatR;
using ShoesAndBlouse.Domain.Entities;
namespace ShoesAndBlouse.Application.Categories.Queries;

public sealed record GetAllCategories : IRequest<IEnumerable<Category>>;