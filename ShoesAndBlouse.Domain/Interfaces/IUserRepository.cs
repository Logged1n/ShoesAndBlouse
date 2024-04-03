using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAll(CancellationToken cancellationToken=default);
        Task<User?> GetUserById(int userId, CancellationToken cancellationToken=default);
        Task<User> CreateUser(User toCreate, CancellationToken cancellationToken = default);
        Task<User?> UpdateUser(User toUpdate, CancellationToken cancellationToken = default);
        Task<User?> DeleteUser(int userId, CancellationToken cancellationToken=default);
    }
}