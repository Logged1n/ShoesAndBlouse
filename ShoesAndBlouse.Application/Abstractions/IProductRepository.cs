using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Application.Abstractions;

public interface IProductRepository
{
    Task<ICollection<Product>> GetAll();
    Task<Product?> GetProductById(int productId);
    Task<Product> CreateProduct(Product toCreate);
    Task<Product> UpdateProduct(int productId, string name, string description, Money price);
    Task<Product?> DeleteProduct(int productId);
}