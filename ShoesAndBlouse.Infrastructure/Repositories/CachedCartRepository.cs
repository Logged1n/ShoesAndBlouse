using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;
using ShoesAndBlouse.Infrastructure.Constants;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CachedCartRepository(IDistributedCache distributedCache) : ICartRepository
{
    public async Task AddItemToCartAsync(int userId, CartItem item, CancellationToken cancellationToken = default)
    {
        var cartToUpdate = await GetCartAsync(userId, cancellationToken);
        if (cartToUpdate is not null)
        {
            string key = CacheKeys.CartByUserId(userId);
            
            cartToUpdate.CartItems.Add(item);
            string cacheValue = JsonConvert.SerializeObject(cartToUpdate);
            await distributedCache.SetStringAsync(key, cacheValue, cancellationToken);
        }
       
    }

    public async Task RemoveItemFromCartAsync(int userId, CartItem item, CancellationToken cancellationToken = default)
    {
        var cartToUpdate = await GetCartAsync(userId, cancellationToken);
        if (cartToUpdate is not null)
        {
            string key = CacheKeys.CartByUserId(userId);
            
            cartToUpdate.CartItems.Remove(item);
            
            string cacheValue = JsonConvert.SerializeObject(cartToUpdate);
            await distributedCache.SetStringAsync(key, cacheValue, cancellationToken);
        }
    }

    public async Task<Cart?> GetCartAsync(int userId, CancellationToken cancellationToken = default)
    {
        string key = $"cart-{userId}";
        //Get the cached value using the key
        string? cachedCart = await distributedCache.GetStringAsync(
            key,
            cancellationToken);
        Cart? cart;
        //if got cache-miss
        if (string.IsNullOrEmpty(cachedCart))
        {
            cart = new Cart();
            //register a new cart in cache
            await distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(cart),
                cancellationToken);
            
            return cart ;
        }
        //if cache-hit, deserialize it and return
        cart = JsonConvert.DeserializeObject<Cart>(
            cachedCart, 
            new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        });
        
        return cart;
    }

    public async Task ClearCartAsync(int userId, CancellationToken cancellationToken = default)
    {
        string key = CacheKeys.CartByUserId(userId);
        string? cachedCart = await distributedCache.GetStringAsync(
            key,
            cancellationToken);
        
        if (!string.IsNullOrEmpty(cachedCart))
        {
            var cart = JsonConvert.DeserializeObject<Cart>(cachedCart);
            if (cart is not null)
                cart.CartItems = [];
            else 
                cart = new Cart();
            
            await distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(cart),
                cancellationToken);
        }
    }
}