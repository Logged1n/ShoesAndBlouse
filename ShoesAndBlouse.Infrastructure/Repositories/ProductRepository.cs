using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class ProductRepository(PostgresDbContext context) : IProductRepository
{
    public async Task<ICollection<Product>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductById(int productId, CancellationToken cancellationToken = default)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public async Task<Product> CreateProduct(Product toCreate, CancellationToken cancellationToken = default)
    {
        context.Products.Add(toCreate);
        await context.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task<Product> UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == toUpdate.Id, cancellationToken);

        if (product is null)
            return await CreateProduct(toUpdate, cancellationToken); //or error
        
        product.Name = toUpdate.Name;
        product.Description = toUpdate.Description;
        product.Price = toUpdate.Price;
        product.Category = toUpdate.Category;
        product.PhotoPath = toUpdate.PhotoPath;

        await context.SaveChangesAsync(cancellationToken);
        
        return product;
    }

    public async Task<bool> DeleteProduct(int productId, CancellationToken cancellationToken = default)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == productId);

        if (product is null) return false;
        
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}