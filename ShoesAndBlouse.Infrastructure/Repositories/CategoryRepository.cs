using System.Collections;
using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CategoryRepository(PostgresDbContext context) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryById(int categoryId, CancellationToken cancellationToken)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
    }

    public async Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken=default)
    {
        context.Categories.Add(toCreate);
        await context.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken=default)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == toUpdate.Id);
        if (category is null) return category;

        category.Name = toUpdate.Name;
        await context.SaveChangesAsync(cancellationToken);
        
        return category;
    }

    public async Task<bool> DeleteCategory(int categoryId, CancellationToken cancellationToken=default)
    {
        var category = context.Categories
            .FirstOrDefault(c => c.Id == categoryId);

        if (category is null) return false;
        
        context.Categories.Remove(category);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Category?> GetCategoryByName(string categoryName, CancellationToken cancellationToken = default)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName, cancellationToken);
    }

    public async Task<ICollection<Category>> GetCategoriesByNames(List<string> categoryNames, CancellationToken cancellationToken = default)
    {
        return await context.Categories
            .Where(category => categoryNames.Contains(category.Name))
            .ToListAsync(cancellationToken);
    }
}


