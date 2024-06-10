using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.ValueObjects;

[ComplexType]
public record Money
{
    public string Currency { get; set; } = "PLN";
    public decimal Amount { get; set; } = 0m;

    public Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
}