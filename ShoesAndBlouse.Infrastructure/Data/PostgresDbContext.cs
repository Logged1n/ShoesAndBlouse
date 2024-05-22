using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Infrastructure.Data;

public class PostgresDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; init; }
    public DbSet<Review> Reviews { get; init; }
    public new DbSet<User> Users { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<Order> Orders { get; init; }
    public DbSet<OrderDetail> OrderDetails { get; init; }
    public DbSet<Address> Addresses { get; init; }
    //Add DbSets here

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Seeding a  'Admin' role to AspNetRoles table
        modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> {Id = 1, Name = "Admin", NormalizedName = "ADMIN".ToUpper() });
        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<User>();
        //Seeding the User to AspNetUsers table
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1, // primary key
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = hasher.HashPassword(null, "Admin1!")
            });
        
        //Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                RoleId = 1, 
                UserId = 1
            });
    }
}