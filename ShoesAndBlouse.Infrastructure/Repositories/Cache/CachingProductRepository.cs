using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Constants;

namespace ShoesAndBlouse.Infrastructure.Repositories.Cache;

public sealed class CachingProductRepository : IProductRepository
{
    private readonly IProductRepository _decorated;
    private readonly IDistributedCache _distributedCache;

    public CachingProductRepository(IProductRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
    }
    
    public async Task<ICollection<Product>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _decorated.GetAll(cancellationToken);
    }

    public async Task<Product?> GetProductById(int productId, CancellationToken cancellationToken = default)
    {
        var key = CacheKeys.ProductById(productId);
        var cachedMember = await _distributedCache.GetStringAsync(key, token: cancellationToken);
        Product? member;
        
        if (string.IsNullOrEmpty(cachedMember))
        {
            member = await _decorated.GetProductById(productId, cancellationToken);

            if (member is null)
                return member;
            
            await _distributedCache.SetStringAsync(
                key,
                JsonSerializer.Serialize(member),
                cancellationToken);
            return member;
        }

        member = JsonSerializer.Deserialize<Product>(cachedMember);
        return member;
    }

    public async Task<Product> CreateProduct(Product toCreate, CancellationToken cancellationToken = default)
    {
        return await _decorated.CreateProduct(toCreate, cancellationToken);
    }

    public async Task<bool> UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default)
    {
        return await _decorated.UpdateProduct(toUpdate, cancellationToken);
    }

    public async Task<bool> DeleteProduct(int productId, CancellationToken cancellationToken = default)
    {
        return await _decorated.DeleteProduct(productId, cancellationToken);
    }
}