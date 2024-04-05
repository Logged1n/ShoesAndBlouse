using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IProductRepository
{
    Task<ICollection<Product>> GetAll(CancellationToken cancellationToken = default);
    Task<Product?> GetProductById(int productId, CancellationToken cancellationToken = default);
    Task<Product> CreateProduct(Product toCreate, CancellationToken cancellationToken = default);
    Task<bool> UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default);
    Task<bool> DeleteProduct(int productId, CancellationToken cancellationToken = default);
}