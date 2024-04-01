using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<ICollection<Domain.Entities.User>> GetAll();
        Task<Domain.Entities.User?> GetUserById(int userId);
        Task<Domain.Entities.User> CreateUser(User toCreate);
        Task<Domain.Entities.User?> UpdateUser(int userId, string imie, string nazwisko, string email);
        Task<Domain.Entities.User?> DeleteUser(int userId);
    }
}