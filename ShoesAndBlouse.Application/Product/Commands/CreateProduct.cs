using ShoesAndBlouse.Domain.Entities;
using MediatR;

namespace ShoesAndBlouse.Application.Product.Commands;

public sealed record CreateProduct(
    string Name,
    string Description) : IRequest<Domain.Entities.Product>;
