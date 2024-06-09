using Microsoft.EntityFrameworkCore;

using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class UserRepository(PostgresDbContext context) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> UpdateUserAsync(User toUpdate, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == toUpdate.Id, cancellationToken);
        if (user is null)
            return null;

        context.Users.Update(toUpdate);
        await context.SaveChangesAsync(cancellationToken);
        
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = context.Users
            .FirstOrDefault(u => u.Id == userId);

        if (user is null) return false;
        
        context.Users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
