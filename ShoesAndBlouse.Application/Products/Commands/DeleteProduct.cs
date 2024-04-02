using MediatR;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Products.Commands;

public sealed record DeleteProduct(int productId) : IRequest<bool>;
