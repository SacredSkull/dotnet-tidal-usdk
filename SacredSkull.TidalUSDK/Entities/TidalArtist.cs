using System;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Entities
{
    public class TidalArtist
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("artistTypes")] public TidalArtistTypes[] Types { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("picture")] public Guid? Image { get; set; }

        [JsonProperty("popularity")] public int Popularity { get; set; }
    }
}