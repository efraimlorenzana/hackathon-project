using Domain;

namespace Application
{
    public static class Utility
    {
        public static string FormatName(Person person)
        {
            var mn = string.Empty;

            if(person.Middlename != null)
            {
                mn = person.Middlename + ". ";
            }

            return person.Firstname + " " + mn + person.Lastname;
        }
    }
}