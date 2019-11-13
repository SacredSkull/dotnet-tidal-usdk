namespace SacredSkull.TidalUSDK.Enums
{
    public class TidalStreamingQualityEnum : StringEnum
    {
        public TidalStreamingQualityEnum(string val) : base(val)
        {
        }

        public static TidalStreamingQualityEnum LOW = new TidalStreamingQualityEnum("LOW");
        public static TidalStreamingQualityEnum HIGH = new TidalStreamingQualityEnum("HIGH");
    }
}