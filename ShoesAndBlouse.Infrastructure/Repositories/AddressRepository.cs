using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class AddressRepository(PostgresDbContext context) : IAddressRepository
{

    public async Task<Address?> GetAddressByValuesAsync(string line1, string line2, string city, string country, string postalCode, CancellationToken cancellationToken = default)
    {
        return await context.Addresses
            .FirstOrDefaultAsync(a => a.Line1 == line1 && a.Line2 == line2 && a.City == city && a.Country == country && a.PostalCode == postalCode, cancellationToken);
    }
    public async Task<Address?> CreateAddressAsync(Address address, CancellationToken cancellationToken = default)
    {
        context.Addresses.Add(address);
        await context.SaveChangesAsync(cancellationToken);
        return address;
    }

}