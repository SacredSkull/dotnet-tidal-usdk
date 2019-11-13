using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Converters {
    internal class CommaSeparatedEnumDeserializer : JsonConverter<IEnumerable<CommaSeparatedStringEnum>> {
        public override void WriteJson(JsonWriter writer, IEnumerable<CommaSeparatedStringEnum> value, JsonSerializer serializer) {
            var buffer = value.Aggregate("", (current, val) => current + $"{val},");
            buffer = buffer.Trim(',');

            writer.WriteValue(buffer);
        }

        public override IEnumerable<CommaSeparatedStringEnum> ReadJson(JsonReader reader, Type objectType, IEnumerable<CommaSeparatedStringEnum> existingValue, bool hasExistingValue,
            JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}