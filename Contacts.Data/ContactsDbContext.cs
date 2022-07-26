using Contacts.Domain.Base;
using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data;

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
        base.OnModelCreating(builder);

        builder.Entity<Audit>().Property(a => a.Id).HasColumnOrder(1);
        builder.Entity<Audit>().Property(a => a.User).HasColumnOrder(2);
        builder.Entity<Audit>().Property(a => a.AuditType).HasColumnOrder(3);
        builder.Entity<Audit>().Property(a => a.EventTime).HasColumnOrder(4);
        builder.Entity<Audit>().Property(a => a.TableName).HasColumnOrder(5);
        builder.Entity<Audit>().Property(a => a.OldRecord).HasColumnOrder(6);
        builder.Entity<Audit>().Property(a => a.NewRecord).HasColumnOrder(7);

        // Unique Columns with SoftDelete
        builder.Entity<Contact>().HasIndex(c => c.Email).HasFilter("IsDeleted = 0").IsUnique();
        builder.Entity<PhoneNumber>().HasIndex(c => c.Number).HasFilter("IsDeleted = 0").IsUnique();

        // Soft deletions on Contacts
        builder.Entity<Contact>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<Person>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<Address>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<PhoneNumber>().HasQueryFilter(m => !m.IsDeleted);
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

            //Audit audit = AuditFactory.CreateFromEntry(entry, user).ToAuditEntity();
            //AuditLogs.Add(audit);
        }
    }

    private void UpdateMetadataColumns()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
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