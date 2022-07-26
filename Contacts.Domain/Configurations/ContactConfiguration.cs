using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Domain.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasOne(c => c.Person)
                .WithOne(p => p.Contact)
                .HasForeignKey(typeof(Person))
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Addresses)
                .WithOne(a => a.Contact)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.PhoneNumbers)
                .WithOne(p => p.Contact)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
