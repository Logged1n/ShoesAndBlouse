using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Entities.Category;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CategoryRepository(PostgresDbContext context) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Category.ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryById(int categoryId, CancellationToken cancellationToken)
    {
        return await context.Category.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
    }

    public async Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken=default)
    {
        context.Category.Add(toCreate);
        await context.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken=default)
    {
        var category = await context.Category.FirstOrDefaultAsync(c => c.Id == toUpdate.Id, cancellationToken);
        if (category is null) 
            return await CreateCategory(toUpdate, cancellationToken);

        category.Name = toUpdate.Name;
        category.Products = toUpdate.Products;
        await context.SaveChangesAsync(cancellationToken);
        
        return category;
    }

    public async Task<Category?> DeleteCategory(int categoryId, CancellationToken cancellationToken=default)
    {
        var category = context.Category.FirstOrDefault(c => c.Id == categoryId);

        if (category is null) return category;
        
        context.Category.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        
        return category;
    }
}


