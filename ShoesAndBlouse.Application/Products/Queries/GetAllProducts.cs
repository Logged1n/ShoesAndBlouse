using MediatR;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetAllProducts : IRequest<IEnumerable<Product>>;