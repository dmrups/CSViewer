using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVOnlineEditor.Interfaces
{
    public interface IValueParser
    {
        Tuple<string, string, string> ParseName(string name);
        DateTime ParseDate(string date);
        string ParseEmail(string email);
        string ParsePhone(string phone);
    }
}
