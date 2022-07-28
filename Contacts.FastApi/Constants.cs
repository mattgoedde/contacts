namespace Contacts.FastApi;

public static partial class Constants
{
    public static class Endpoints
    {
        public static class Entities
        {
            public const string People = "people";
            public const string Contact = "contacts";
            public const string Address = "addresses";
            public const string PhoneNumber = "phonenumbers";
        }
        public static class People
        {
            public const string CertainPeople = "people";
            public const string AllPeople = "people/all";
            public const string PersonById = "people/{id:int}";
        }

        public static class Addresses
        {
            public const string CertainAddresses = "addresses";
            public const string AllAddresses = "addresses/all";
            public const string AddressById = "addresses/{id:int}";
        }

        public static class PhoneNumbers
        {
            public const string CertainPhoneNumbers = "phonenumbers";
            public const string AllPhoneNumbers = "phonenumbers/all";
            public const string PhoneNumberById = "phonenumbers/{id:int}";
        }

        public static class Contacts
        {
            public const string CertainContacts = "contacts";
            public const string AllContacts = "contacts/all";
            public const string ContactById = "contacts/{id:int}";
        }
    }
}