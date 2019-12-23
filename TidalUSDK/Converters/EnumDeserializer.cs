using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TidalUSDK.Enums;

namespace TidalUSDK.Converters {
    internal class EnumSerializer : JsonConverter<Enum> {
        public override void WriteJson(JsonWriter writer, Enum value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override Enum ReadJson(JsonReader reader, Type objectType, Enum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}