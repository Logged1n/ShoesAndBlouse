namespace ShoesAndBlouse.Application.DTOs;

public class ReviewDto
{
    public int Id { get; init; }
    public int Score { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}