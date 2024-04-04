using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.ValueObjects;

[ComplexType]
public record Money(string Currency, decimal Amount);