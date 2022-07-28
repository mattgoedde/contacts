using Contacts.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Data.Database.Configurations
{
    public class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T>
        where T : EntityBase, new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasQueryFilter(m => !m.IsDeleted);
        }
    }
}
