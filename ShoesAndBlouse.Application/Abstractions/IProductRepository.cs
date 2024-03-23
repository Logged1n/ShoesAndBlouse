using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Abstractions;

public interface IProductRepository
{
    Task<ICollection<Product>> GetAll(CancellationToken cancellationToken = default);
    Task<Product?> GetProductById(int productId, CancellationToken cancellationToken = default);
    Task<Product> CreateProduct(Product toCreate, CancellationToken cancellationToken = default);
    Task<Product> UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default);
    Task<Product?> DeleteProduct(int productId, CancellationToken cancellationToken = default);
}