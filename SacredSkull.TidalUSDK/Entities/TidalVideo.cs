using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Entities
{
    public class TidalVideo
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("volumeNumber")] public int VolumeNumber { get; set; }

        [JsonProperty("trackNumber")] public int TrackNumber { get; set; }

        [JsonProperty("releaseDate")] public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("imagePath")] public string CoverPath { get; set; }

        [JsonProperty("imageId")] public Guid? Cover { get; set; }

        [JsonProperty("duration")] public int Duration { get; set; }

        [JsonProperty("quality")] public string Quality { get; set; }

        [JsonProperty("streamReady")] public bool IsReadyToStream { get; set; }

        [JsonProperty("streamStartDate")] public DateTimeOffset StreamingAvailableSince { get; set; }

        [JsonProperty("allowStreaming")] public bool StreamingAllowed { get; set; }

        [JsonProperty("explicit")] public bool Explicit { get; set; }

        [JsonProperty("popularity")] public int Popularity { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("adsUrl")] public string AdsUrl { get; set; }

        [JsonProperty("adsPrePaywallOnly")] public bool AdsPrePaywallOnly { get; set; }

        [JsonProperty("artists")] public IEnumerable<TidalNestedArtist> Artists { get; set; }

        [JsonProperty("album")] public TidalNestedAlbum Album { get; set; }
    }
}