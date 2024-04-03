using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Entities.Category;

namespace ShoesAndBlouse.Application.Abstractions;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAll(CancellationToken cancellationToken=default);
    Task<Category?> GetCategoryById(int categoryId, CancellationToken cancellationToken=default);
    Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken=default);
    Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken=default);
    Task<Category?> DeleteCategory(int productId, CancellationToken cancellationToken=default);
}