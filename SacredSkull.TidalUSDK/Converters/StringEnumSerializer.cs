using System;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Converters
{
    public class StringEnumSerializer : JsonConverter<StringEnum>
    {
        public override void WriteJson(JsonWriter writer, StringEnum stringEnum, JsonSerializer serializer)
        {
            writer.WriteValue(stringEnum.Value);
        }

        public override StringEnum ReadJson(JsonReader reader, Type objectType, StringEnum existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}