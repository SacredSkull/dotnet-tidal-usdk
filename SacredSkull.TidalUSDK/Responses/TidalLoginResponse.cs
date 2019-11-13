using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Responses
{
    public class TidalLoginResponse
    {
        [JsonProperty("countryCode")] public string CountryCode;

        [JsonProperty("sessionId")] public string SessionId;

        [JsonProperty("streamQuality")] public string StreamQuality;

        [JsonProperty("userId")] public string UserId;
    }
}