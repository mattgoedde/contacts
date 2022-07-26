using Contacts.Domain.Models;

namespace Contacts.Data.Entities;

public record AddressEntity : Address, IEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public int ContactId { get; set; }
    public ContactEntity? Contact { get; set; }
    public bool IsDeleted { get; set; }

    public IEntity Update(IEntity entity)
    {
        if (entity is AddressEntity address)
        {
            address.StreetNumber = address.StreetNumber;
            address.StreetNumber2 = address.StreetNumber2;
            address.City = address.City;
            address.State = address.State;
            address.Country = address.Country;
            address.ZipCode = address.ZipCode;
            return address;
        }
        else throw new ArgumentException($"Entity was not of type {typeof(AddressEntity)}", nameof(entity));
    }
}