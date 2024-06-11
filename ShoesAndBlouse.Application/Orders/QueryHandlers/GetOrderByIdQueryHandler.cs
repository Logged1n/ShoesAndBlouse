using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Orders.Queries;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Orders.QueryHandlers;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);
        if(order is not null)
            return OrderMapper.MapToDto(order);
        
        return new OrderDto(
            "0",
            DateTime.Now,
            DateTime.Now,
            OrderStatus.Cancelled,
            "0",
            null,
            "0",
            [],
            new Money(
                "PLN",
                0m
            ));
        
    }
}