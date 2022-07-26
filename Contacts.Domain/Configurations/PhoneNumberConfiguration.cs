using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Domain.Configurations
{
    public class PhoneNumberConfiguration : BaseEntityConfiguration<PhoneNumber>
    {
        public override void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            base.Configure(builder);
        }
    }
}
