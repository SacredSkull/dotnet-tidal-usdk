using Newtonsoft.Json;

namespace TidalUSDK.Entities {
    public class TidalNestedArtist {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("type")]
        public string Type;
    }
}