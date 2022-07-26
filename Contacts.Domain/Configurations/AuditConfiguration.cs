using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Domain.Configurations
{
    public class AuditConfiguration : BaseEntityConfiguration<Audit>
    {
        public override void Configure(EntityTypeBuilder<Audit> builder)
        {
            base.Configure(builder);
        }
    }
}
