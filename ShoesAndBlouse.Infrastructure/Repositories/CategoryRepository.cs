using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CategoryRepository(PostgresDbContext context) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetAll()
    {
        return await context.Category.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int categoryId)
    {
        return await context.Category.FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken=default)
    {
        context.Category.Add(toCreate);
        await context.SaveChangesAsync();
        return toCreate;
    }

    public async Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken=default)
    {
        var category = await context.Category.FirstOrDefaultAsync(c => c.Id == toUpdate.Id);
        if (category is null) return category;

        category.Name = toUpdate.Name;
        await context.SaveChangesAsync();
        
        return category;
    }

    public async Task<Category?> DeleteCategory(int categoryId, CancellationToken cancellationToken=default)
    {
        var category = context.Category
            .FirstOrDefault(c => c.Id == categoryId);

        if (category is null) return null;
        
        context.Category.Remove(category);

        await context.SaveChangesAsync();
        return category;
    }
}


