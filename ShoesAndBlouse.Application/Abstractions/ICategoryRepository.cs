using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Abstractions;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAll();
    Task<Category?> GetCategoryById(int categoryId);
    Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken=default);
    Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken=default);
    Task<Category?> DeleteCategory(int productId, CancellationToken cancellationToken=default);
}