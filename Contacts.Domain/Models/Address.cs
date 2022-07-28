using Contacts.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Models;

[Table(Constants.Schema.Addresses.TableName, Schema = Constants.Schema.SchemaName)]
public class Address : EntityBase
{
    [Required]
    public string StreetNumber { get; set; }
    public string StreetNumber2 { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string ZipCode { get; set; }

    public Contact Contact { get; set; }
}