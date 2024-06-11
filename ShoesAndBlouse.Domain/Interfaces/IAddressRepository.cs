using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IAddressRepository
{
    Task<Address?> GetAddressByValuesAsync(string line1, string line2, string city, string country, string postalCode, CancellationToken cancellationToken = default);
    Task<Address?> CreateAddressAsync(Address address, CancellationToken cancellationToken = default);
}