namespace ShoesAndBlouse.Application.DTOs;

public class ReviewDto
{
    public string Id { get; init; }
    public int Score { get; set; }
    public string ProductId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
  
}