using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetAllProductsQuery : IRequest<IEnumerable<Product>>;