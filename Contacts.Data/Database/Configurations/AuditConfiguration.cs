using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Data.Database.Configurations
{
    public class AuditConfiguration : BaseEntityConfiguration<Audit>
    {
        public override void Configure(EntityTypeBuilder<Audit> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Id).HasColumnOrder(1);
            builder.Property(a => a.User).HasColumnOrder(2);
            builder.Property(a => a.AuditType).HasColumnOrder(3);
            builder.Property(a => a.EventTime).HasColumnOrder(4);
            builder.Property(a => a.TableName).HasColumnOrder(5);
            builder.Property(a => a.OldRecord).HasColumnOrder(6);
            builder.Property(a => a.NewRecord).HasColumnOrder(7);
        }
    }
}
