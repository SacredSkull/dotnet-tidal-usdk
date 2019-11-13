namespace SacredSkull.TidalUSDK.Enums
{
    public class TidalFilterTypes : CommaSeparatedStringEnum
    {
        public static TidalFilterTypes All = new TidalFilterTypes("ALL");

        private TidalFilterTypes(string val) : base(val)
        {
        }
    }
}