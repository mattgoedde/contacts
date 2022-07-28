using Contacts.Data.Database.Configurations;
using Contacts.Domain.Base;
using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data.Database;

public static class ContactsDbContextExtensions
{
    public static ContactsDbContext SetUser(this ContactsDbContext context, string username)
    {
        context.CurrentUser = username;
        return context;
    }
}

public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
    : base(options)
    { }

    public string CurrentUser;

    public DbSet<Audit> AuditLogs { get; set; } = default!;
    public DbSet<Contact> Contacts { get; set; } = default!;
    public DbSet<Person> People { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<PhoneNumber> PhoneNumbers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuditConfiguration());
        builder.ApplyConfiguration(new ContactConfiguration());
        builder.ApplyConfiguration(new PersonConfiguration());
        builder.ApplyConfiguration(new AddressConfiguration());
        builder.ApplyConfiguration(new PhoneNumberConfiguration());
    }

    public override int SaveChanges()
    {
        CreateAuditTrail(CurrentUser);
        UpdateMetadataColumns();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        CreateAuditTrail(CurrentUser);
        UpdateMetadataColumns();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void CreateAuditTrail(string user = null)
    {
        ChangeTracker.DetectChanges();
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is Audit) continue;
            if (entry.State == EntityState.Detached) continue;
            if (entry.State == EntityState.Unchanged) continue;

            Audit audit = AuditLogFactory.FromEntry(entry, user);
            AuditLogs.Add(audit);
        }
    }

    private void UpdateMetadataColumns()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is EntityBase entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedOn = DateTime.Now;
                        entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedOn = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entity.DeletedOn = DateTime.Now;
                        entity.IsDeleted = true;
                        break;
                    default: break;
                }
            }
            else
            {
                if (entry.Entity is not Audit)
                    throw new DbUpdateException("Entry did not inherit from IEntity");
            }
        }
    }
}