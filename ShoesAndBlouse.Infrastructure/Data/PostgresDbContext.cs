using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Infrastructure.Data;

public class PostgresDbContext : IdentityDbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Address> Addresses { get; set; }
    //Add DbSets here
}