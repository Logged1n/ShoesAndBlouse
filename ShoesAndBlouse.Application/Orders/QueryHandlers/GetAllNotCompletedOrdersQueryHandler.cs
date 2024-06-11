using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Orders.Queries;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Orders.QueryHandlers;

public class GetAllNotCompletedOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetAllNotCompletedOrdersQuery, List<OrderDto>>
{

    public async Task<List<OrderDto>> Handle(GetAllNotCompletedOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllNotCompletedAsync(cancellationToken);
        return OrderMapper.MapToDtos(orders);
    }
}