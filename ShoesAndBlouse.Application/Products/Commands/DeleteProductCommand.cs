using MediatR;

namespace ShoesAndBlouse.Application.Products.Commands;

public sealed record DeleteProductCommand(int ProductId) : IRequest;
