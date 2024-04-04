using System.Collections;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAll(CancellationToken cancellationToken);
    Task<Category?> GetCategoryById(int categoryId, CancellationToken cancellationToken);
    Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken = default);
    Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken = default);
    Task<bool> DeleteCategory(int productId, CancellationToken cancellationToken = default);
    Task<Category?> GetCategoryByName(string categoryName, CancellationToken cancellationToken = default);

    Task<ICollection<Category>> GetCategoriesByNames(List<string> categoryNames,
        CancellationToken cancellationToken = default);
}