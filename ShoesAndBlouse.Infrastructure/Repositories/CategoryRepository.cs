using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class CategoryRepository(PostgresDbContext context) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await context.Categories
            .Include(c => c.Products)
            .ToListAsync(cancellationToken);

        return categories;
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .Include(category => category.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
        return category;
    }

    public async Task CreateCategoryAsync(Category toCreate, CancellationToken cancellationToken=default)
    {
        context.Categories.Add(toCreate);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken=default)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);

        if (category is null) return;
        
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateCategoryAsync(Category toUpdate, CancellationToken cancellationToken=default)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == toUpdate.Id, cancellationToken);
        
        if (category is null) 
            return;

        context.Categories.Update(toUpdate);
        await context.SaveChangesAsync(cancellationToken);
        

    }
    
}


