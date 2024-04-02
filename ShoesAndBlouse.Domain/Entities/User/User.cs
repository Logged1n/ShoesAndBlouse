using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ShoesAndBlouse.Domain.Entities.User;

public sealed class User
{
    [Key]
    public int Id { get; set; }
    public string Imie { get; set; } = string.Empty;
    public string Nazwisko { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; }
}