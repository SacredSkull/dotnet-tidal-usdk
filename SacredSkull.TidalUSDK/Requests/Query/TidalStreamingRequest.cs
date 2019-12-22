using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Requests
{
    public class TidalStreamingRequest : TidalRequest
    {
        [JsonProperty("soundQuality")]
        public TidalStreamingQualityEnum StreamQuality;
    }
}