using Contacts.Domain.Models;

namespace Contacts.Data.Entities;

public record PhoneNumberEntity : PhoneNumber, IEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public int ContactId { get; set; }
    public ContactEntity? Contact { get; set; } = default!;
    public bool IsDeleted { get; set; }

    public IEntity Update(IEntity entity)
    {
        if (entity is PhoneNumberEntity phoneNumber)
        {
            Number = phoneNumber.Number;

            return this;
        }
        else throw new ArgumentException($"Entity was not of type {typeof(PhoneNumberEntity)}", nameof(entity));
    }
}