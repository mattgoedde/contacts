using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Data.Database.Configurations
{
    public class PersonConfiguration : EntityBaseConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);
        }
    }
}
