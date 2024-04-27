using System.Collections;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll(CancellationToken cancellationToken=default);
    Task<Category> GetCategoryById(int categoryId, CancellationToken cancellationToken=default);
    Task CreateCategory(Category toCreate, CancellationToken cancellationToken = default);
    Task DeleteCategory(int productId, CancellationToken cancellationToken = default);
    Task UpdateCategory(Category toUpdate, CancellationToken cancellationToken = default);
}