using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.DTOs;

public record OrderDto(
    string Id,
    DateTime CreatedAt,
    DateTime ModifiedAt,
    OrderStatus Status,
    string UserId,
    string? ShippingAddressId,
    string BillingAddressId,
    List<OrderDetailDto> OrderDetails,
    Money Total);