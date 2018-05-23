using System.ComponentModel;

namespace SMGS.Presentation.Classes
{
    public class Enums
    {
        public enum Account
        {
            SupperAdmin = 1,
            Admin = 2,
            Customer = 3,
            OtherUsers = 4
        }

        public enum ContactFor
        {
            Staff = 1,
            Custommer = 2
        }

        public enum ContactType
        {
            Address = 1,
            EAddress = 2
        }

        public enum Language
        {
            English = 1,
            Vietnamese = 2
        }
    }
}