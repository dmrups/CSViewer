using System.Collections.Generic;

namespace CSVOnlineEditor.Interfaces
{
    public interface ICSVSerializer
    {
        string Serialize<T>(IEnumerable<T> collection, IAccessor<T> accessor);

        IEnumerable<T> Deserialize<T>(string collection, IBuilder<T> factory);
    }
}
