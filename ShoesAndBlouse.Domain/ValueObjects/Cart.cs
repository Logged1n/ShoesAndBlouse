using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace ShoesAndBlouse.Domain.ValueObjects;

[ComplexType]
public sealed class Cart
{
    public List<CartItem> CartItems { get; set; } = [];
    public Money Total { get; set; } = new Money ("PLN", 0m);
}