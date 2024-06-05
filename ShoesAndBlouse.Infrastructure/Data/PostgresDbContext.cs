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
        //Seeding roles and test users for demo
        // Seeding roles to AspNetRoles table
        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<int> { Id = 2, Name = "Manager", NormalizedName = "MANAGER" },
            new IdentityRole<int> { Id = 3, Name = "Client", NormalizedName = "CLIENT" });

        // A hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<User>();

        // Seeding the User to AspNetUsers table
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1, // primary key
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin1!"),
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 2, // primary key
                UserName = "manager@manager.com",
                Email = "manager@manager.com",
                NormalizedUserName = "MANAGER@MANAGER.COM",
                NormalizedEmail = "MANAGER@MANAGER.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Manager1!"),
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 3, // primary key
                UserName = "client@client.com",
                Email = "client@client.com",
                NormalizedUserName = "CLIENT@CLIENT.COM",
                NormalizedEmail = "CLIENT@CLIENT.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Client1!"),
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

        // Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
            new IdentityUserRole<int> {RoleId = 2, UserId = 2},
            new IdentityUserRole<int> {RoleId = 3, UserId = 3});
    }
}
