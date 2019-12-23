using System;
using Newtonsoft.Json;
using TidalUSDK.Enums;

namespace TidalUSDK.Entities
{
    public class TidalPlaylist
    {
        [JsonProperty("uuid")]
		public Guid Id { get; set; }

        [JsonProperty("title")]
		public string Title { get; set; }

        [JsonProperty("numberOfTracks")]
		public int TrackCount { get; set; }

        [JsonProperty("numberOfVideos")]
		public int VideoCount { get; set; }

        [JsonProperty("creator", NullValueHandling = NullValueHandling.Ignore)]
        public TidalPlaylistCreator Creator { get; set; }

        [JsonProperty("description")]
		public string Description { get; set; }

        [JsonProperty("lastUpdated")]
		public DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("created")]
		public DateTimeOffset Created { get; set; }

        [JsonProperty("type")]
		public TidalPlaylistType Type { get; set; }

        [JsonProperty("publicPlaylist")]
		public bool Public { get; set; }

        [JsonProperty("url")]
		public string Url { get; set; }

        [JsonProperty("popularity")]
		public int Popularity { get; set; }

        [JsonProperty("squareImage")]
		public Guid? Cover { get; set; }

        public class TidalPlaylistCreator
        {
            [JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

	        [JsonProperty("artistTypes")]
			public TidalArtistTypes[] Types { get; set; }

	        [JsonProperty("url")]
			public string Url { get; set; }

	        [JsonProperty("picture")]
			public Guid? Cover { get; set; }

	        [JsonProperty("popularity")]
			public int Popularity { get; set; }
        }
    }
}