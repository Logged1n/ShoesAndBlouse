using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Review
{
    [Key]
    public int Id { get; set; }
    public int Score { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
   
}