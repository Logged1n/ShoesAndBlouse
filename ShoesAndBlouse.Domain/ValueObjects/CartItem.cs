using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.ValueObjects;

[ComplexType]
public class CartItem : IEquatable<CartItem>
{
    public int ProductId { get; init; }
    public int Qty { get; set; }
    public  bool Equals(CartItem? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return ProductId == other.ProductId;
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != this.GetType())
            return false;
        return Equals((CartItem)obj);
    }
    public override int GetHashCode()
    {
        return ProductId;
    }
}