using CSVOnlineEditor.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CSVOnlineEditor.JsonConverters
{
    public class DateOnly : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dateString = reader.ReadAsString();
            return ServicesHelper.Parser.ParseDate(dateString);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dateString = ServicesHelper.Serializer.SerializeDate((DateTime)value);
            JToken.FromObject(dateString).WriteTo(writer);
        }
    }
}
