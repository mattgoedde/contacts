namespace Contacts.Domain.Models;

public record Contact
{
    public string Email { get; set; } = string.Empty;
    public Person Person { get; set; } = default!;
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
}