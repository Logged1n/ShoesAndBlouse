using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAll(CancellationToken cancellationToken = default);
    Task<Product> GetProductById(int productId, CancellationToken cancellationToken = default);
    Task CreateProduct(Product toCreate, CancellationToken cancellationToken = default);
    Task UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default);
    Task DeleteProduct(int productId, CancellationToken cancellationToken = default);
    int GetNextProductId();
}