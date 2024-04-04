using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class UserRepository(PostgresDbContext context) : IUserRepository
{
    public async Task<ICollection<User>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Users.ToListAsync(cancellationToken);
    }
    public async Task<User?> GetUserById(int userId, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User> CreateUser(User toCreate, CancellationToken cancellationToken=default)
    {
        context.Users.Add(toCreate);
        await context.SaveChangesAsync(cancellationToken);
        return toCreate;
    }

    public async Task<User?> UpdateUser(User toUpdate, CancellationToken cancellationToken=default)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == toUpdate.Id, cancellationToken);
        if (user is null)
        {
            return await CreateUser(toUpdate, cancellationToken);
        }
        user.Name = toUpdate.Name;
        user.Surname = toUpdate.Surname;
        user.Email = toUpdate.Email;
        user.Address = toUpdate.Address;
        await context.SaveChangesAsync(cancellationToken);
        
        return user;
    }

    public async Task<bool> DeleteUser(int userId, CancellationToken cancellationToken=default)
    {
        var user = context.Users
            .FirstOrDefault(u => u.Id == userId);

        if (user is null) return false;
        
        context.Users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
