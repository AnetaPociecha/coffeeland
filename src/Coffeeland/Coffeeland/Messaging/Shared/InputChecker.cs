using System.Text.RegularExpressions;

namespace Coffeeland.Messaging.Shared
{
    public static class InputChecker
    {
        public static bool isValidEmail(string email)
        {
            var regex = @"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            var match = Regex.Match(email, regex, RegexOptions.IgnoreCase);

            return match.Success;
        }

        public static bool isValidName(string name)
        {
            var regex = @"^[A-z]{2,20}$";
            var match = Regex.Match(name, regex, RegexOptions.IgnoreCase);

            return match.Success;
        }

        public static bool isValidApartmentNumber(string apartmentNumber)
        {
            var regex = @"^[1-9]*[a-zA-Z]{0,1}$";
            var match = Regex.Match(apartmentNumber, regex, RegexOptions.IgnoreCase);

            return match.Success;
        }
    }
}