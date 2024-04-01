using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class User
{
    public int Id { get; set; }
    public string Imie { get; set; } = string.Empty;
    public string Nazwisko { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}