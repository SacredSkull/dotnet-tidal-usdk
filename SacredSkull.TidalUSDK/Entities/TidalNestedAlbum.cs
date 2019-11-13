using System;
using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Entities
{
    public class TidalNestedAlbum
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("cover")] public Guid Cover { get; set; }

        [JsonProperty("releaseDate")] public DateTime ReleaseDate { get; set; }
    }
}