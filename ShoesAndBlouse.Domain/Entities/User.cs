using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; }
    public List<Order> Orders { get; set; }
    public List<Review> Reviews { get; set; }
}