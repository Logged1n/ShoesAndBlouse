using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Mappers;

public static class AddressMapper
{
    public static AddressDto MapToDto(Address address)
    {
        return new AddressDto
        {
            Line1 = address.Line1,
            Line2 = address.Line2,
            City = address.City,
            Country = address.Country,
            PostalCode = address.PostalCode
        };
    }
}