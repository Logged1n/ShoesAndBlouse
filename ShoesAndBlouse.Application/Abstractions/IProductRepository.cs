using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Abstractions;

public interface IProductRepository
{
    Task<ICollection<Domain.Entities.Product>> GetAll();
    Task<Domain.Entities.Product?> GetProductById(int productId);
    Task<Domain.Entities.Product> CreateProduct(Domain.Entities.Product toCreate);
    Task<Domain.Entities.Product?> UpdateProduct(int productId, string name, string description);
    Task<Domain.Entities.Product?> DeleteProduct(int productId);
}