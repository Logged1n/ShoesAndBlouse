using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Infrastructure.Data;

public class UserDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public UserDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("Default"));
    }
    
    public DbSet<User> User { get; set; }
}