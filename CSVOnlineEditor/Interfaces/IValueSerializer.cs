using System;

namespace CSVOnlineEditor.Interfaces
{
    public interface IValueSerializer
    {
        string SerializeName (Tuple<string, string, string> name);
        string SerializeDate (DateTime date);
    }
}
