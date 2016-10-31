using CSVOnlineEditor.Interfaces;
using System;

namespace CSVOnlineEditor.Helpers
{
    [Obsolete]
    public static class ServicesHelper
    {
        public static IValueParser Parser;
        public static IValueSerializer Serializer;
    }
}
