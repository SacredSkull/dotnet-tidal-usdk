using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TidalUSDK.Entities
{
    public class TidalAlbum
    {
        [JsonProperty("id")]
		public string Id { get; set; }

        [JsonProperty("title")]
		public string Title { get; set; }

        [JsonProperty("duration")]
		public int Duration { get; set; }

        [JsonProperty("streamReady")]
		public bool IsReadyToStream { get; set; }

        [JsonProperty("streamStartDate")]
		public DateTimeOffset? StreamingAvailableSince { get; set; }

        [JsonProperty("allowStreaming")]
		public bool StreamingAllowed { get; set; }

        [JsonProperty("premiumStreamingOnly")]
		public bool PremiumStreamingOnly { get; set; }

        [JsonProperty("numberOfTracks")]
		public int TrackCount { get; set; }

        [JsonProperty("numberOfVideos")]
		public int VideoCount { get; set; }

        [JsonProperty("numberOfVolumes")]
		public int VolumeCount { get; set; }

        [JsonProperty("releaseDate")]
		public DateTime ReleaseDate { get; set; }

        [JsonProperty("copyright")]
		public string Copyright { get; set; }

        [JsonProperty("type")]
		public string Type { get; set; }

        [JsonProperty("version")]
		public string Version { get; set; }

        [JsonProperty("url")]
		public string Url { get; set; }

        [JsonProperty("cover")]
		public Guid Cover { get; set; }

        [JsonProperty("explicit")]
		public bool Explicit { get; set; }

        [JsonProperty("upc")]
		public string UPC { get; set; }

        [JsonProperty("popularity")]
		public int Popularity { get; set; }

        //[JsonProperty("audioQuality")]
        //public TidalQuality Quality { get; set; }

        //[JsonProperty("surroundTypes")]
        //public IEnumerable<TidalSurround> SurroundTypes { get; set; }

        [JsonProperty("artists")]
		public IEnumerable<TidalNestedArtist> ArtistIds { get; set; }

		[JsonIgnore] public TidalNestedArtist MainArtist => ArtistIds.FirstOrDefault(artist => artist.Type == "MAIN");
    }
}