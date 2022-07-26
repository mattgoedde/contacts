namespace Contacts.Domain.Models;

public record Person
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
}