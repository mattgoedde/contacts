using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Contacts.Data;

public static class AuditLogFactory
{
    public static Audit FromEntry(EntityEntry entry, string user = "")
    {
        Dictionary<string, object> oldRecord = new();
        Dictionary<string, object> newRecord = new();

        Audit log = new Audit()
        {
            EventTime = DateTime.Now,
            User = user,
            AuditType = entry.State.ToString(),
            TableName = entry.Entity.GetType().Name
        };

        foreach (var property in entry.Properties)
        {
            oldRecord[property.Metadata.Name] = entry.OriginalValues[property.Metadata.Name];
            newRecord[property.Metadata.Name] = entry.OriginalValues[property.Metadata.Name];
        }

        switch (entry.State)
        {
            case EntityState.Added:
                log.OldRecord = null;
                log.NewRecord = newRecord;
                break;
            case EntityState.Deleted:
                log.OldRecord = oldRecord;
                log.NewRecord = null;
                break;
            case EntityState.Modified:
                log.OldRecord = oldRecord;
                log.NewRecord = newRecord;
                break;
            default:
                break;
        }

        return log;
    }
}