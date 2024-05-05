using Microsoft.AspNetCore.Identity;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class  User : IdentityUser<int>
{
    [PersonalData]
    public string Name { get; set; } = string.Empty;
    [PersonalData]
    public string Surname { get; set; } = string.Empty;
    [PersonalData]
    public Address? Address { get; set; }
    [PersonalData]
    public List<Order> Orders { get; set; } = [];
    [PersonalData]
    public List<Review> Reviews { get; set; } = [];
}