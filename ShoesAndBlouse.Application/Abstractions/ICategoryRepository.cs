using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Abstractions;

public interface ICategoryRepository
{
    Task<ICollection<Domain.Entities.Category>> GetAll();
    Task<Domain.Entities.Category?> GetCategoryById(int categoryId);
    Task<Domain.Entities.Category> CreateCategory(Domain.Entities.Category toCreate);
    Task<Domain.Entities.Category?> UpdateCategory(int productId, string name);
    Task<Domain.Entities.Category?> DeleteCategory(int productId);
}