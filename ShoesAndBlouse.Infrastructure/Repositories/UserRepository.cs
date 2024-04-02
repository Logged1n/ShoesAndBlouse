using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Entities.User;
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

    public async Task<User> CreateUser(User toCreate, CancellationToken cancellationToken=default)
    {
        context.User.Add(toCreate);
        await context.SaveChangesAsync();
        return toCreate;
    }

    public async Task<User?> UpdateUser(User toUpdate, CancellationToken cancellationToken=default)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.Id == toUpdate.Id);
        if (user is null)
        {
            return await CreateUser(toUpdate, cancellationToken);
        }
        user.Imie = toUpdate.Imie;
        user.Nazwisko = toUpdate.Nazwisko;
        user.Email = toUpdate.Email;
        user.Address = toUpdate.Address;
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> DeleteUser(int userId, CancellationToken cancellationToken=default)
    {
        var user = context.User
            .FirstOrDefault(u => u.Id == userId);

        if (user is null) return null;
        
        context.User.Remove(user);

        await context.SaveChangesAsync();
        return user;
    }
}
