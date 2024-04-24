using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class ProductRepository(PostgresDbContext context) : IProductRepository
{
    public async Task<ICollection<Product>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Products.ToListAsync(cancellationToken); // Return list of all products from database
    }

    public async Task<Product?> GetProductById(int productId, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FindAsync(productId, cancellationToken);
        return product ; // return first found product with provided Id
    }

    public async Task<Product> CreateProduct(Product toCreate, CancellationToken cancellationToken = default)
    {
        context.Products.Add(toCreate); //Add product
        await context.SaveChangesAsync(cancellationToken); // Save changes to database
        return toCreate; // return saved product
    }

    public async Task<bool> UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == toUpdate.Id, cancellationToken); // Find product to update

        if (product is null)
            return false; //If it was not found
        //else set new Product Props
        //product.Name = toUpdate.Name; 
        //product.Description = toUpdate.Description;
        //product.Price = toUpdate.Price;
        //product.Categories = toUpdate.Categories;
        //product.PhotoPath = toUpdate.PhotoPath;
        context.Products.Update(toUpdate);
        await context.SaveChangesAsync(cancellationToken); // save changes to database
        
        return true; // update succeeded!
    }

    public async Task<bool> DeleteProduct(int productId, CancellationToken cancellationToken = default)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == productId); // find the product

        if (product is null) return false; // if it was not found delete failed
        
        context.Products.Remove(product);  // else remove it
        await context.SaveChangesAsync(cancellationToken); // and save changes to databse
        
        return true; // delete succeeded!
    }
}