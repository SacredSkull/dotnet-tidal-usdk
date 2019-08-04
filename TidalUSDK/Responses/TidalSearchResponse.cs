using System.Collections.Generic;
using Newtonsoft.Json;
using TidalUSDK.Entities;
using TidalUSDK.Utilities;

namespace TidalUSDK.Responses {
    [JsonConverter(typeof(JSONPathConverter))]
    public class TidalSearchResponse {
        [JsonProperty("artists.items")]
        public IEnumerable<TidalArtist> Artists { get; set; }
        
        [JsonProperty("albums.items")]
        public IEnumerable<TidalAlbum> Albums { get; set; }
        
        [JsonProperty("tracks.items")]
        public IEnumerable<TidalTrack> Tracks { get; set; }
        
        [JsonProperty("playlists.items")]
        public IEnumerable<TidalPlaylist> Playlists { get; set; }
        
        [JsonProperty("videos.items")]
        public IEnumerable<TidalVideo> Videos { get; set; }
        
        [JsonProperty("topHit")]
        public TidalTopHit TopHit { get; set; }
    }
}