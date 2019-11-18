using System;
using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Entities
{
    public class TidalArtistBio
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}