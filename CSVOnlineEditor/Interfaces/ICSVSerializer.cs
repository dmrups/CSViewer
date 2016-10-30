using System.Collections.Generic;

namespace CSVOnlineEditor.Interfaces
{
    public interface ICSVSerializer
    {
        string Serialize(ICollection<ICSVSerializable> collection);

        IEnumerable<T> Deserialize<T>(string collection, IBuilder<T> factory);
    }
}
