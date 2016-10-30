using CSVOnlineEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSVOnlineEditor.Serializers
{
    public class CSVSerializer : ICSVSerializer
    {
        public IEnumerable<T> Deserialize<T>(string collection, IBuilder<T> factory)
        {
            foreach(var row in collection.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                yield return factory.CreateObject(row.Split('\t'));
            }
        }
        
        public string Serialize(ICollection<ICSVSerializable> collection)
        {
            return string.Join("\r\n", collection.Select(item => string.Join("\t", item.GetObjectData())));
        }
    }
}
