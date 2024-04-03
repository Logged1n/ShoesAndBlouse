using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities.Product;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class ProductRepository(PostgresDbContext context) : IProductRepository
{
    public async Task<ICollection<Product>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Product.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductById(int productId, CancellationToken cancellationToken = default)
    {
        return await context.Product.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public async Task<Product> CreateProduct(Product toCreate, CancellationToken cancellationToken = default)
    {
        context.Product.Add(toCreate);
        await context.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task<Product> UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default)
    {
        var product = await context.Product.FirstOrDefaultAsync(p => p.Id == toUpdate.Id, cancellationToken);

        if (product is null)
            return await CreateProduct(toUpdate, cancellationToken); //or error
        
        product.Name = toUpdate.Name;
        product.Description = toUpdate.Description;
        product.Price = toUpdate.Price;
        product.PhotoPath = toUpdate.PhotoPath;
        product.Category = toUpdate.Category;
        await context.SaveChangesAsync(cancellationToken);
        
        return product;
    }

    public async Task<Product?> DeleteProduct(int productId, CancellationToken cancellationToken = default)
    {
        var product = context.Product.FirstOrDefault(p => p.Id == productId);

        if (product is null) return product;
        
        context.Product.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        
        return product;
    }
}