using System.Collections.Generic;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Entities;

namespace SacredSkull.TidalUSDK.Responses
{
    public class TidalTopTracksResponse
    {
        [JsonProperty("items")] public IEnumerable<TidalTrack> Tracks { get; set; }
    }
}