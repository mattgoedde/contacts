using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Contacts.Domain.Configurations
{
    public class AddressConfiguration : BaseEntityConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);
        }
    }
}
