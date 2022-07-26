using Contacts.Data.Entities;

namespace Contacts.Data;

public static class DbInitializer
{
    public static bool Initialize(ContactsDbContext context)
    {
        try
        {
            context.Database.EnsureCreated();

            // Look for any Contacts.
            if (context.Contacts.Any())
            {
                return true;   // DB has been seeded
            }

            return true;
        }
        catch
        {
            return false;
        }

    }
}