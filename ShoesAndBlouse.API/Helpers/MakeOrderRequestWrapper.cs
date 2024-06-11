using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.API.Helpers;

public record MakeOrderRequestWrapper
{
    public AddressDto BillingAddress { get; set; }
    public AddressDto? ShippingAddress { get; set; }
}