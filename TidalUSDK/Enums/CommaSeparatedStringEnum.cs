using Newtonsoft.Json;

namespace TidalUSDK.Enums {
    public abstract class CommaSeparatedStringEnum : StringEnum {
        protected CommaSeparatedStringEnum(string val) : base(val) {}
    }
}