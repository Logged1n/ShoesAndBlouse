using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Constants;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CachedCartRepository(IDistributedCache distributedCache,
    IUserRepository userRepository) : ICartRepository
{
    public async Task AddItemToCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default)
    {
        var cartToUpdate = await GetCartAsync(userId, cancellationToken);
        if (cartToUpdate is not null)
        {
            string key = CacheKeys.CartByUserId(userId);

            if (cartToUpdate.CartItems.Contains(item))
                throw new Exception("No changes to the cart!");
            cartToUpdate.CartItems.Add(item);
            
            string cacheValue = JsonConvert.SerializeObject(cartToUpdate);
            await distributedCache.SetStringAsync(key, cacheValue, cancellationToken);
        }
       
    }

    public async Task RemoveItemFromCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default)
    {
        var cartToUpdate = await GetCartAsync(userId, cancellationToken);
        if (cartToUpdate is not null)
        {
            string key = CacheKeys.CartByUserId(userId);

            if (!cartToUpdate.CartItems.Contains(item))
                throw new Exception("No changes to the cart!");
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
            //register a new cart in cache
            await distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(cachedCart),
                cancellationToken);
            
            return new Cart(await userRepository.GetUserByIdAsync(userId, cancellationToken));
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

    public Task MakeOrderAsync(Cart userCart, CancellationToken cancellationToken = default)
    {
        //TODO shouldn't it be UserRepository responsibility?
        throw new NotImplementedException();
    }
}