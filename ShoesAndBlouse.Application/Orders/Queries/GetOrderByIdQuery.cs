using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Orders.Queries;

public sealed record GetOrderByIdQuery(int OrderId) : IRequest<OrderDto>;