using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class  User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IdentityUser UserCredentials { get; set; }
    public Address? Address { get; set; }
    public List<Order> Orders { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
}