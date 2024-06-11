using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Orders.Queries;

public sealed record GetAllUserOrdersQuery : IRequest<List<OrderDto>>
{
    public int UserId { get; set; }
}