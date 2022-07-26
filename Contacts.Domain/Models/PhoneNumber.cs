using Contacts.Domain.Base;
using Contacts.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Models;

[Table(Constants.Schema.PhoneNumbers.TableName, Schema = Constants.Schema.SchemaName)]
[EntityTypeConfiguration(typeof(PhoneNumberConfiguration))]
public class PhoneNumber : BaseEntity
{
    [Phone]
    [Required]
    public string Number { get; set; }
    public Contact Contact { get; set; }
}
