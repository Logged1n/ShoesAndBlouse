using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Orders.Queries;

public sealed record GetAllNotCompletedOrdersQuery() : IRequest<List<OrderDto>>;