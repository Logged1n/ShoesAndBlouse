namespace ShoesAndBlouse.Domain.ValueObjects;

public record CartItem
{
    public int ProductId { get; set; }
    public int Qty { get; set; }
}