using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAll();
    Task<Category?> GetCategoryById(int categoryId);
    Task<Category> CreateCategory(Category toCreate, CancellationToken cancellationToken = default);
    Task<Category?> UpdateCategory(Category toUpdate, CancellationToken cancellationToken = default);
    Task<Category?> DeleteCategory(int productId, CancellationToken cancellationToken = default);
    Task<Category?> GetCategoryByName(string categoryName, CancellationToken cancellationToken = default);

    Task<ICollection<Category>> GetCategoriesByNames(List<string> categoryNames,
        CancellationToken cancellationToken = default);
}