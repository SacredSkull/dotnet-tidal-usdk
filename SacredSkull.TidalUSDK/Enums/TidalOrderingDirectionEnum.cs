namespace SacredSkull.TidalUSDK.Enums
{
    public class TidalOrderingDirectionEnum : StringEnum
    {
        public TidalOrderingDirectionEnum(string val) : base(val)
        {
        }

        public static readonly TidalOrderingDirectionEnum Ascending = new TidalOrderingDirectionEnum("ASC");
        public static readonly TidalOrderingDirectionEnum Descending = new TidalOrderingDirectionEnum("DESC");
    }
}