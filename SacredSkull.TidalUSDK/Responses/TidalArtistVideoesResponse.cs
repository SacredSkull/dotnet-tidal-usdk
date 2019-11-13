using System.Collections.Generic;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Entities;

namespace SacredSkull.TidalUSDK.Responses
{
    public class TidalArtistVideoesResponse
    {
        [JsonProperty("totalNumberOfItems")]
        public int TotalNumber { get; set; }

        [JsonProperty("items")]
        public IEnumerable<TidalVideo> Videos { get; set; }
    }
}