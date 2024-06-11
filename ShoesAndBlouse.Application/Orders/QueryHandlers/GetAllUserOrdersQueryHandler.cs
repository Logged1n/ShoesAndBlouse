using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Orders.Queries;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Orders.QueryHandlers;

public class GetAllUserOrdersQueryHandler(IOrderRepository orderRepository,
    IUserRepository userRepository) : IRequestHandler<GetAllUserOrdersQuery, List<OrderDto>>
{

    public async Task<List<OrderDto>> Handle(GetAllUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return [];
        }
        var orders = await orderRepository.GetAllUserOrderAsync(user, cancellationToken);
        return OrderMapper.MapToDtos(orders);
    }
}