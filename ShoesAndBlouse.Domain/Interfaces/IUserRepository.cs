using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken=default);
        Task<User> CreateUserAsync(User toCreate, CancellationToken cancellationToken = default);
        Task<User?> UpdateUserAsync(User toUpdate, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken=default);
    }
}