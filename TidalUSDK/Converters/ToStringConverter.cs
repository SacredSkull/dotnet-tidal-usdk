using System;
using Newtonsoft.Json;

namespace TidalUSDK.Converters
{
    public class ToStringConverter : JsonConverter<IFormattable>
    {
        public override void WriteJson(JsonWriter writer, IFormattable value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override IFormattable ReadJson(JsonReader reader, Type objectType, IFormattable existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}