using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TidalUSDK.Entities;

namespace TidalUSDK.Responses {
    public class TidalTopTracksResponse {
        [JsonProperty("items")]
        public IEnumerable<TidalTrack> Tracks { get; set; }
    }
}