using Contacts.Domain.Base;
using Contacts.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Domain.Models;

[Table(Constants.Schema.AuditLogs.TableName, Schema = Constants.Schema.SchemaName)]
[EntityTypeConfiguration(typeof(AuditConfiguration))]
public class Audit : BaseEntity
{
    public DateTime EventTime { get; set; }
    public string User { get; set; }
    public string AuditType { get; set; }
    public string TableName { get; set; }
    public Dictionary<string, object> OldRecord { get; set; }
    public Dictionary<string, object> NewRecord { get; set; }
}