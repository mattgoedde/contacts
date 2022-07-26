using System.Text.Json;
using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Contacts.Data.Entities;

public record AuditEntity : Audit
{
    public new string? OldRecord { get; set; }
    public new string? NewRecord { get; set; }
}

public static class AuditExtensions
{
    public static AuditEntity ToAuditEntity(this Audit audit)
        => new()
        {
            Id = audit.Id,
            EventTime = audit.EventTime,
            User = audit.User,
            AuditType = audit.AuditType,
            TableName = audit.TableName,
            OldRecord = audit.OldRecord?.Any() ?? false ? JsonSerializer.Serialize(audit.OldRecord) : null,
            NewRecord = audit.NewRecord?.Any() ?? false ? JsonSerializer.Serialize(audit.NewRecord) : null
        };
}

public static class AuditFactory
{
    public static Audit CreateFromEntry(EntityEntry entry, string? user)
    {
        var audit = new Audit
        {
            EventTime = DateTime.Now,
            User = user,
            TableName = entry.Entity.GetType().Name,
            AuditType = entry.State.ToString(),
            OldRecord = new(),
            NewRecord = new()
        };

        foreach (var property in entry.Properties)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    audit.NewRecord[property.Metadata.Name] = property.CurrentValue;
                    break;
                case EntityState.Modified:
                    audit.OldRecord[property.Metadata.Name] = property.OriginalValue;
                    audit.NewRecord[property.Metadata.Name] = property.CurrentValue;
                    break;
                case EntityState.Deleted:
                    audit.OldRecord[property.Metadata.Name] = property.OriginalValue;
                    break;
            }
        }

        switch (entry.State)
        {
            case EntityState.Added:
                audit.OldRecord = null;
                break;
            case EntityState.Modified:
                break;
            case EntityState.Deleted:
                audit.NewRecord = null;
                break;
        }

        return audit;
    }
}