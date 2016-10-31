using CSVOnlineEditor.Interfaces;
using System;

namespace CSVOnlineEditor.Serializers
{
    public class ValueSerializer : IValueSerializer
    {
        public string SerializeDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public string SerializeName(Tuple<string, string, string> name)
        {
            return $"{name.Item1} {name.Item2} {name.Item3}".Trim();
        }
    }
}
