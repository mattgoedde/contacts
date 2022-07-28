using Contacts.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Models;

[Table(Constants.Schema.PhoneNumbers.TableName, Schema = Constants.Schema.SchemaName)]
public class PhoneNumber : EntityBase
{
    [Phone]
    [Required]
    public string Number { get; set; }
    public Contact Contact { get; set; }
}
