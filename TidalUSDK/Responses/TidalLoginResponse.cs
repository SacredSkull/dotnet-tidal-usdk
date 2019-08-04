using Newtonsoft.Json;

namespace TidalUSDK.Responses {
    public class TidalLoginResponse {
        [JsonProperty("sessionId")]
        public string SessionId;
        
        [JsonProperty("userId")]
        public string UserId;
        
        [JsonProperty("countryCode")]
        public string CountryCode;
        
        [JsonProperty("streamQuality")]
        public string StreamQuality;
    }
}