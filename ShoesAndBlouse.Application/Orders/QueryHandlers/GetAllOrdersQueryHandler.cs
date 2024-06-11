using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Orders.Queries;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Orders.QueryHandlers;

public class GetAllOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);

        return OrderMapper.MapToDtos(orders);
    }
}