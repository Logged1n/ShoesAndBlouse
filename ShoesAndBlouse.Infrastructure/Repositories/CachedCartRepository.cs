using Microsoft.Extensions.Caching.Distributed;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CachedCartRepository : ICartRepository
{
    private readonly IDistributedCache _distributedCache;

    public CachedCartRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public async Task AddItemToCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemFromCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EditItemInCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> GetCartAsync(int userId, CancellationToken cancellationToken = default)
    {
        string key = $"cart-{userId}";
        string? cachedCart = await _distributedCache.GetStringAsync(
            key,
            cancellationToken);

        //if (string.IsNullOrEmpty(cachedCart))
        //{
            
        //}
        return new Cart();
    }

    public Task MakeOrderAsync(Cart userCart, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}