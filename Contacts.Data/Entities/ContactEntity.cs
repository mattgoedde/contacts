using Contacts.Domain.Models;

namespace Contacts.Data.Entities;

public record ContactEntity : Contact, IEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public new PersonEntity Person { get; set; } = default!;
    public new ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
    public new ICollection<PhoneNumberEntity> PhoneNumbers { get; set; } = new List<PhoneNumberEntity>();
    public bool IsDeleted { get; set; }

    private ICollection<T> Update<T>(ICollection<T> oldEntities, ICollection<T> newEntities)
        where T : class, IEntity, new()
    {
        if (!newEntities.Any() && !oldEntities.Any()) return oldEntities;

        foreach (var e in newEntities)
        {
            if (e is T t
                && entities.Any(a => a.Id == t.Id)) // If already in list, update entity
            {
                entities.Single(a => a.Id == t.Id).Update(t);
            }
            else if (e is T newT
                && !entities.Any(a => a.Id == newPnEntity.Id)) // If not in list, add entity
            {
                entities.Add(newPnEntity);
            }
        }
        // Remove no longer existing entity
        var removedEntities = entities.AsEnumerable().ExceptBy<T, int>(entities.Select(a => a.Id), a => a.Id);
        foreach (var e in removedEntities.ToList())
            entities.Remove(e);

        return entities;
    }

    public IEntity Update(IEntity entity)
    {
        if (entity is ContactEntity contact)
        {
            contact.Email = contact.Email;

            if (contact.Person is PersonEntity personEntity)
                contact.Person = (PersonEntity)contact.Person.Update(personEntity);

            PhoneNumbers =

            foreach (var address in contact.Addresses)
            {
                if (address is AddressEntity adEntity && contact.Addresses.Any(pn => pn.Id == adEntity.Id))
                {
                    contact.Addresses.Single(ad => ad.Id == adEntity.Id).Update(adEntity);
                }
                else if (address is AddressEntity newAdEntity && !contact.Addresses.Any(pn => pn.Id == newAdEntity.Id))
                {
                    contact.Addresses.Add(newAdEntity);
                }
            }

            var removedAddresses = Addresses.ExceptBy<AddressEntity, int>(contact.Addresses.Select(ad => ad.Id), ad => ad.Id);
            foreach (var address in removedAddresses.ToList())
                Addresses.Remove(address);

            return this;
        }
        else throw new ArgumentException($"Entity was not of type {typeof(ContactEntity)}", nameof(entity));
    }
}
