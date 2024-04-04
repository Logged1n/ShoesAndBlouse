using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesAndBlouse.Domain.Entities.User;

[ComplexType]
public record Address(string City, string PostalCode, string Country, string Telephone);
