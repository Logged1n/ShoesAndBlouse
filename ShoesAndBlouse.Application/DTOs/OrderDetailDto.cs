namespace ShoesAndBlouse.Application.DTOs;

public record OrderDetailDto(
    string Id,
    string OrderId,
    string ProductId,
    string Qty);