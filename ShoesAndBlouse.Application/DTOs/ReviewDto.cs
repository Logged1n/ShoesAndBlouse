using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.DTOs;

public class ReviewDto
{
    public string Id { get; init; }
    public int Score { get; set; }
    public Product ProductId { get; set; }
    public User UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
  
}