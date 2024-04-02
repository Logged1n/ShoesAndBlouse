using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Entities.User;

namespace ShoesAndBlouse.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAll();
        Task<User?> GetUserById(int userId);
        Task<User> CreateUser(User toCreate, CancellationToken cancellationToken = default);
        Task<User?> UpdateUser(User toUpdate, CancellationToken cancellationToken = default);
        Task<User?> DeleteUser(int userId, CancellationToken cancellationToken=default);
    }
}