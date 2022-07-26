
namespace Contacts.Domain
{
    public static partial class Constants
    {
        public static class Schema
        {
            public const string SchemaName = "dbo";
            public static class Contacts
            {
                public const string TableName = "Contacts";
                public const string ContactsPersonIdIndex = "IX_Contacts_PersonId";
            }
            public static class People
            {
                public const string TableName = "People";
            }
            public static class AuditLogs
            {
                public const string TableName= "AuditLogs";
            }
            public static class Addresses
            {
                public const string TableName = "Addresses";
            }
            public static class PhoneNumbers
            {
                public const string TableName = "PhoneNumbers";
            }
        }
    }
}
