using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SacredSkull.TidalUSDK.Responses {
    public class TidalArtistBioResponse {
        [JsonProperty("source")]
        public string Source { get; set; }
        
        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}