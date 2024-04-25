using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Products.Queries;

public sealed record GetAllProductsQuery : IRequest<List<ProductDto>>;