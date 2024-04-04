using MediatR;

namespace ShoesAndBlouse.Application.Products.Commands;

public sealed record DeleteProduct(int productId) : IRequest<bool>;
