using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.Entities.Product;

[ComplexType]
public record Money(string Currency, decimal Amount);