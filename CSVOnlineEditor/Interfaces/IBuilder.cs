using System.Collections.Generic;

namespace CSVOnlineEditor.Interfaces
{
    public interface IBuilder<T>
    {
        T CreateObject(string[] fields);
        T CreateObject(Dictionary<string, string> fields);
    }
}
