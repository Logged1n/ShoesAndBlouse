using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IProductRepository
{
    Task<ICollection<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<int> CreateProductAsync(Product toCreate, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(Product toUpdate, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(int productId, CancellationToken cancellationToken = default);
    int GetNextProductId();
}