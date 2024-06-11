using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Review
{
    [Key]
    public int Id { get; init; }
    public int Score { get; set; }
    public Product Product { get; set; }
    public User User { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}