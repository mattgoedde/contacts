using Contacts.Data.Entities;
using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data;

public static class ContactsDbContextExtensions
{
    public static ContactsDbContext SetUser(this ContactsDbContext context, string? username)
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

    public string? CurrentUser;

    public DbSet<AuditEntity> AuditLogs { get; set; } = default!;
    public DbSet<ContactEntity> Contacts { get; set; } = default!;
    public DbSet<PersonEntity> People { get; set; } = default!;
    public DbSet<AddressEntity> Addresses { get; set; } = default!;
    public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ContactEntity>().ToTable("Contacts");
        builder.Entity<PersonEntity>().ToTable("People");
        builder.Entity<AddressEntity>().ToTable("Addresses");
        builder.Entity<PhoneNumberEntity>().ToTable("PhoneNumbers");
        builder.Entity<AuditEntity>().ToTable("AuditLogs");

        builder.Entity<AuditEntity>().Property(a => a.Id).HasColumnOrder(1);
        builder.Entity<AuditEntity>().Property(a => a.User).HasColumnOrder(2);
        builder.Entity<AuditEntity>().Property(a => a.AuditType).HasColumnOrder(3);
        builder.Entity<AuditEntity>().Property(a => a.EventTime).HasColumnOrder(4);
        builder.Entity<AuditEntity>().Property(a => a.TableName).HasColumnOrder(5);
        builder.Entity<AuditEntity>().Property(a => a.OldRecord).HasColumnOrder(6);
        builder.Entity<AuditEntity>().Property(a => a.NewRecord).HasColumnOrder(7);

        //Navigation from Contacts to Sub-Classes
        builder.Entity<ContactEntity>().Navigation(c => c.Person).AutoInclude();
        builder.Entity<ContactEntity>().Navigation(c => c.Addresses).AutoInclude();
        builder.Entity<ContactEntity>().Navigation(c => c.PhoneNumbers).AutoInclude();

        // One to many relation
        builder.Entity<ContactEntity>().HasMany(c => c.Addresses).WithOne(a => a.Contact);
        builder.Entity<ContactEntity>().HasMany(c => c.PhoneNumbers).WithOne(p => p.Contact);

        // Unique Columns with SoftDelete
        builder.Entity<ContactEntity>().HasIndex(c => c.Email).HasFilter("IsDeleted = 0").IsUnique();
        builder.Entity<PhoneNumberEntity>().HasIndex(c => c.Number).HasFilter("IsDeleted = 0").IsUnique();

        // Soft deletions on Contacts
        builder.Entity<ContactEntity>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<PersonEntity>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<AddressEntity>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<PhoneNumberEntity>().HasQueryFilter(m => !m.IsDeleted);
    }

    public override int SaveChanges()
    {
        CreateAuditTrail(CurrentUser);
        UpdateMetadataColumns();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        CreateAuditTrail(CurrentUser);
        UpdateMetadataColumns();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void CreateAuditTrail(string? user = null)
    {
        ChangeTracker.DetectChanges();
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is Audit) continue;
            if (entry.Entity is AuditEntity) continue;
            if (entry.State == EntityState.Detached) continue;
            if (entry.State == EntityState.Unchanged) continue;

            AuditEntity audit = AuditFactory.CreateFromEntry(entry, user).ToAuditEntity();
            AuditLogs.Add(audit);
        }
    }

    private void UpdateMetadataColumns()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is IEntity entity)
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
                if (entry.Entity is not AuditEntity)
                    throw new DbUpdateException("Entry did not inherit from IEntity");
            }
        }
    }
}