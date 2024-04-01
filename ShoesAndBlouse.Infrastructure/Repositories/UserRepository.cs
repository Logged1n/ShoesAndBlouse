using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class UserRepository(PostgresDbContext context) : IUserRepository
{
    public async Task<ICollection<User>> GetAll()
    {
        return await context.User.ToListAsync();
    }
    public async Task<User?> GetUserById(int userId)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User> CreateUser(User toCreate)
    {
        context.User.Add(toCreate);
        await context.SaveChangesAsync();
        return toCreate;
    }

    public async Task<User?> UpdateUser(int userId, string imie, string nazwisko, string email)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return user;
        
        user.Imie = imie;
        user.Nazwisko = nazwisko;
        user.Email = email;
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> DeleteUser(int userId)
    {
        var user = context.User
            .FirstOrDefault(u => u.Id == userId);

        if (user is null) return null;
        
        context.User.Remove(user);

        await context.SaveChangesAsync();
        return user;
    }
}
