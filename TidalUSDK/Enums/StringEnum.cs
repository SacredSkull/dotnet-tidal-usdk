using System;
using Newtonsoft.Json;
using TidalUSDK.Converters;

namespace TidalUSDK.Enums
{
    [JsonConverter(typeof(ToStringConverter))]
    public abstract class StringEnum : IFormattable
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

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Value;
        }
    }
}