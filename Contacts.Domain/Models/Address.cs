namespace Contacts.Domain.Models;

public record Address
{
    public string StreetNumber { get; set; } = string.Empty;
    public string? StreetNumber2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}