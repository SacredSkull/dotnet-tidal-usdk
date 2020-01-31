using System.Collections.Generic;
using Newtonsoft.Json;
using TidalUSDK.Entities;
using TidalUSDK.Utilities;

namespace TidalUSDK.Responses
{
    public class TidalSearchResponse
    {
        [JsonProperty("artists")]
        public TidalPaginatedResponse<TidalArtist> Artists { get; set; }

        [JsonProperty("albums")]
        public TidalPaginatedResponse<TidalAlbum> Albums { get; set; }

        [JsonProperty("tracks")]
        public TidalPaginatedResponse<TidalTrack> Tracks { get; set; }

        [JsonProperty("playlists")]
        public TidalPaginatedResponse<TidalPlaylist> Playlists { get; set; }

        [JsonProperty("videos")]
        public TidalPaginatedResponse<TidalVideo> Videos { get; set; }

        [JsonProperty("topHit")]
        public TidalTopHit TopHit { get; set; }
    }
}