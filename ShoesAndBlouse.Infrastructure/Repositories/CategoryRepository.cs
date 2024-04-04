using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CategoryRepository(PostgresDbContext context) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetAll()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int categoryId)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken=default)
    {
        context.Categories.Add(toCreate);
        await context.SaveChangesAsync();
        return toCreate;
    }

    public async Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken=default)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == toUpdate.Id);
        if (category is null) return category;

        category.Name = toUpdate.Name;
        await context.SaveChangesAsync();
        
        return category;
    }

    public async Task<Category?> DeleteCategory(int categoryId, CancellationToken cancellationToken=default)
    {
        var category = context.Categories
            .FirstOrDefault(c => c.Id == categoryId);

        if (category is null) return null;
        
        context.Categories.Remove(category);

        await context.SaveChangesAsync();
        return category;
    }

    public Task<Category?> GetCategoryByName(string categoryName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category?>> GetCategoriesByNames(List<string> categoryNames, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}


