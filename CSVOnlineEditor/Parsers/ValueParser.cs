using CSVOnlineEditor.Interfaces;
using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace CSVOnlineEditor.Parsers
{
    public class ValueParser : IValueParser
    {
        private Regex notNumberRegex = new Regex(@"[^\d]");
        private Regex extraSpacesRegex = new Regex("[ ]{2,}");

        /// <summary>
        /// Splits full name into last, first, middle
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tuple<string, string, string> ParseName(string name)
        {
            var splitted = extraSpacesRegex.Replace(name, " ").Trim().Split(' ');

            if (splitted.Length == 1)
            {
                return new Tuple<string, string, string>(splitted[0], "", "");
            }
            else if (splitted.Length == 2)
            {
                return new Tuple<string, string, string>(splitted[0], splitted[1], "");
            }

            return new Tuple<string, string, string>(splitted[0], splitted[1], splitted[2]);
        }

        public DateTime ParseDate(string date)
        {
            DateTime result;

            DateTime.TryParse(date, out result);

            if (result < SqlDateTime.MinValue.Value)
            {
                result = SqlDateTime.MinValue.Value;
            }

            if (result > SqlDateTime.MaxValue.Value)
            {
                result = SqlDateTime.MaxValue.Value;
            }

            return result;
        }

        /// <summary>
        /// Cleans up email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ParseEmail(string email)
        {
            return email.Replace(" ", "").Trim().ToLower();
        }

        /// <summary>
        /// Cleans up phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string ParsePhone(string phone)
        {
            return notNumberRegex.Replace(phone, "");
        }
    }
}
