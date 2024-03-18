using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CategoryRepository(CategoryDbContext context) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetAll()
    {
        return await context.Category.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int categoryId)
    {
        return await context.Category.FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<Category> CreateCategory(Category toCreate)
    {
        context.Category.Add(toCreate);
        await context.SaveChangesAsync();
        return toCreate;
    }

    public async Task<Category?> UpdateCategory(int categoryId, string name)
    {
        var category = await context.Category.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category is null) return category;

        category.Name = name;
        await context.SaveChangesAsync();
        
        return category;
    }

    public async Task<Category?> DeleteCategory(int categoryId)
    {
        var category = context.Category
            .FirstOrDefault(c => c.Id == categoryId);

        if (category is null) return null;
        
        context.Category.Remove(category);

        await context.SaveChangesAsync();
        return category;
    }
}


