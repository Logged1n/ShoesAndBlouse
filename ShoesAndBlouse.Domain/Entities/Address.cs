using System.ComponentModel.DataAnnotations;

namespace ShoesAndBlouse.Domain.Entities;

public record Address
{
    [Key]
    public int Id { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; } = string.Empty;
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}
  
    
