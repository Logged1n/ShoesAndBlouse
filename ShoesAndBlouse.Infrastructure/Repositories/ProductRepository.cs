﻿using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class ProductRepository(PostgresDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetAll(CancellationToken cancellationToken = default)
    {
        var products = await context.Products
            .Include(p => p.Categories)
            .ToListAsync(cancellationToken);

        return products;
    }

    public async Task<Product> GetProductById(int productId, CancellationToken cancellationToken = default)
    {
        var product = await context.Products
            .Include(product => product.Categories)
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        return product ; // return first found product with provided Id
    }

    public async Task CreateProduct(Product toCreate, CancellationToken cancellationToken = default)
    {
        context.Products.Add(toCreate); //Add product
        await context.SaveChangesAsync(cancellationToken); // Save changes to database
    }

    public async Task UpdateProduct(Product toUpdate, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == toUpdate.Id, cancellationToken); // Find product to update

        if (product is null)
            return; //If it was not found
        
        context.Products.Update(toUpdate);
        await context.SaveChangesAsync(cancellationToken); // save changes to database
    }

    public async Task DeleteProduct(int productId, CancellationToken cancellationToken = default)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == productId); // find the product

        if (product is null) return; // if it was not found delete failed
        
        context.Products.Remove(product);  // else remove it
        await context.SaveChangesAsync(cancellationToken); // and save changes to databse
    }
}