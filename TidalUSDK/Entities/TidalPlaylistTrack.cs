using System;
using Newtonsoft.Json;

namespace TidalUSDK.Entities
{
    public class TidalPlaylistTrack : TidalTrack
    {
        [JsonProperty("dateAdded")]
        public DateTimeOffset DateAdded { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }
    }
}