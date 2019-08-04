namespace TidalUSDK.Enums {
    public class TidalFilterTypes : CommaSeparatedStringEnum {
        private TidalFilterTypes(string val) : base(val) {}

        public static TidalFilterTypes All = new TidalFilterTypes("ALL");
    }
}