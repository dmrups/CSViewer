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
                var fields = row.Split('\t');

                if(fields.Length < 4)
                {
                    continue;
                }

                yield return factory.CreateObject(fields);
            }
        }
        
        public string Serialize<T>(IEnumerable<T> collection, IAccessor<T> accessor)
        {
            return string.Join("\r\n", 
                collection.Select(item => string.Join("\t", 
                    accessor.GetObjectData(item))));
        }
    }
}
