using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Data.Database.Configurations
{
    public class PhoneNumberConfiguration : EntityBaseConfiguration<PhoneNumber>
    {
        public override void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            base.Configure(builder);

            // unique phone numbers where not deleted
            builder.HasIndex(c => c.Number).HasFilter("IsDeleted = 0").IsUnique();
        }
    }
}
