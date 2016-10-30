using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace CSVOnlineEditor.Helpers
{
    public static class ParseHelper
    {
        private static Regex notNumberRegex = new Regex(@"[^\d]");
        private static Regex extraSpacesRegex = new Regex("[ ]{2,}");

        /// <summary>
        /// Splits full name into last, first, middle
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Tuple<string, string, string> ParseName(string name)
        {
            var splitted = extraSpacesRegex.Replace(name, " ").Trim().Split(' ');

            if (splitted.Length == 0)
            {
                return new Tuple<string, string, string>(null, null, null);
            }
            else if (splitted.Length == 1)
            {
                return new Tuple<string, string, string>(splitted[0], null, null);
            }
            else if (splitted.Length == 2)
            {
                return new Tuple<string, string, string>(splitted[0], splitted[1], null);
            }

            return new Tuple<string, string, string>(splitted[0], splitted[1], splitted[2]);
        }

        public static DateTime ParseDate(string date)
        {
            DateTime result;

            if (DateTime.TryParse(date, out result))
            {
                return result;
            }
            else
            {
                return SqlDateTime.MinValue.Value;
            }
        }

        /// <summary>
        /// Cleans up email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string ParseEmail(string email)
        {
            return email.Replace(" ", "").Trim().ToLower();
        }

        /// <summary>
        /// Cleans up phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string ParsePhone(string phone)
        {
            return notNumberRegex.Replace(phone, "");
        }
    }
}
