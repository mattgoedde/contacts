using Contacts.Domain.Models;

namespace Contacts.Data.Entities;

public record PersonEntity : Person, IEntity
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
        if (entity is PersonEntity person)
        {
            FirstName = person.FirstName;
            MiddleName = person.MiddleName;
            LastName = person.LastName;
            Birthdate = person.Birthdate;

            return this;
        }
        else throw new ArgumentException($"Entity was not of type {typeof(PersonEntity)}", nameof(entity));
    }
}