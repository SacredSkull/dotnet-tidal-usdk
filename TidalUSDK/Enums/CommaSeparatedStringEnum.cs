using Newtonsoft.Json;
using TidalUSDK.Serializers;

namespace TidalUSDK.Enums {
    public abstract class CommaSeparatedStringEnum : StringEnum {
        protected CommaSeparatedStringEnum(string val) : base(val) {}
    }
}