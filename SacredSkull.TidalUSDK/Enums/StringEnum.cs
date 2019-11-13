using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Converters;

namespace SacredSkull.TidalUSDK.Enums
{
    [JsonConverter(typeof(StringEnumSerializer))]
    public abstract class StringEnum
    {
        protected StringEnum(string val)
        {
            Value = val;
        }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }
}