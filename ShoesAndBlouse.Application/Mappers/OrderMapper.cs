using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Mappers;

public static class OrderMapper
{
    private static OrderDetailDto MapDetailToDto(OrderDetail? orderDetail)
    {
        if (orderDetail is null)
        {
            return new OrderDetailDto("0", "0", "0", "0");
        }

        return new OrderDetailDto(
            orderDetail.Id.ToString(),
            orderDetail.Order?.Id.ToString() ?? "0",
            orderDetail.Product?.Id.ToString() ?? "0",
            orderDetail.Qty.ToString()
        );
    }

    private static List<OrderDetailDto> MapDetailsToDtos(ICollection<OrderDetail> orderDetails)
    {
        return orderDetails.Select(MapDetailToDto).ToList();
    }

    public static OrderDto MapToDto(Order order)
    {
        return new OrderDto(
            order.Id.ToString(),
            order.CreatedAt,
            order.ModifiedAt,
            order.Status,
            order.User?.Id.ToString() ?? "0",
            order.ShippingAddress?.Id.ToString() ?? "0",
            order.BillingAddress?.Id.ToString() ?? "0",
            MapDetailsToDtos(order.OrderDetails),
            order.Total
        );
    }

    public static List<OrderDto> MapToDtos(ICollection<Order> orders)
    {
        return orders.Select(MapToDto).ToList();
    }

    public static Order MapCartToOrder(Cart cart, User user, List<OrderDetail> orderDetails, Address? shippingAddress, Address? billingAddress)
    {
        return new Order
        {
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            Status = OrderStatus.Open,
            ShippingAddress = shippingAddress ?? user.Address,
            BillingAddress = billingAddress ?? user.Address ?? new Address(),
            OrderDetails = orderDetails,
            Total = cart.Total,
        };
    }
}