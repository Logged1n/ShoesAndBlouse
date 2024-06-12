using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken=default);
        Task<ICollection<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<User?> UpdateUserAsync(User toUpdate, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}