using Contacts.Domain.Base;
using Contacts.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Models;

[Table(Constants.Schema.Contacts.TableName, Schema = Constants.Schema.SchemaName)]
[Index(nameof(PersonId), Name = Constants.Schema.Contacts.ContactsPersonIdIndex)]
[EntityTypeConfiguration(typeof(ContactConfiguration))]
public class Contact : BaseEntity
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public int PersonId { get; set; }

    public Person Person { get; set; }

    public IEnumerable<Address> Addresses { get; set; }

    public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
}