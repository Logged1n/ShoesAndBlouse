using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<Category?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken=default);
    Task CreateCategoryAsync(Category toCreate, CancellationToken cancellationToken = default);
    Task DeleteCategoryAsync(int productId, CancellationToken cancellationToken = default);
    Task UpdateCategoryAsync(Category toUpdate, CancellationToken cancellationToken = default);
}