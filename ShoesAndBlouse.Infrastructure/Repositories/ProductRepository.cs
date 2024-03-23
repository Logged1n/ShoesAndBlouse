using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities.Product;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class ProductRepository(PostgresDbContext context) : IProductRepository
{
    public async Task<ICollection<Product>> GetAll()
    {
        return await context.Product.ToListAsync();
    }

    public async Task<Product?> GetProductById(int productId)
    {
        return await context.Product.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product> CreateProduct(Product toCreate)
    {
        context.Product.Add(toCreate);
        await context.SaveChangesAsync();
        return toCreate;
    }

    public async Task<Product> UpdateProduct(int productId, string name, string description, Money price)
    {
        var product = await context.Product.FirstOrDefaultAsync(p => p.Id == productId);
        if (product is null) return product;
        
        product.Name = name;
        product.Description = description;
        product.Price = price; 
        await context.SaveChangesAsync();
        
        return product;
    }

    public async Task<Product?> DeleteProduct(int productId)
    {
        var product = context.Product
            .FirstOrDefault(p => p.Id == productId);

        if (product is null) return product;
        
        context.Product.Remove(product);

        await context.SaveChangesAsync();
        return product;
    }
}