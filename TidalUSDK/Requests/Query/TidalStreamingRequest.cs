using Newtonsoft.Json;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests
{
    public class TidalStreamingRequest : TidalRequest
    {
        [JsonProperty("soundQuality")]
        public TidalStreamingQualityEnum StreamQuality;
    }
}