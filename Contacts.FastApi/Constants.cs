namespace Contacts.FastApi;

public static partial class Constants
{
    public static class Endpoints
    {
        public static class Entities
        {
            public const string People = "people";
            public const string Contact = "contacts";
        }
        public static class People
        {
            public const string CertainPeople = "people";
            public const string AllPeople = "people/all";
            public const string PersonById = "people/{id:int}";
        }

        public static class Contacts
        {
            public const string CertainContacts = "contacts";
            public const string AllContacts = "contacts/all";
            public const string ContactById = "contacts/{id:int}";
        }
    }
}