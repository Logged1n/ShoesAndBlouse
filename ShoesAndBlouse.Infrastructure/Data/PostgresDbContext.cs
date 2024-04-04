using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Infrastructure.Data;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    //Add DbSets here
}