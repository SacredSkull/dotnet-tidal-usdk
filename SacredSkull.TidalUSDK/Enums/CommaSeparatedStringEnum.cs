using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Enums {
    public abstract class CommaSeparatedStringEnum : StringEnum {
        protected CommaSeparatedStringEnum(string val) : base(val) {}
    }
}