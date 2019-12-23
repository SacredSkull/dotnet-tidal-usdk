using Newtonsoft.Json;

namespace TidalUSDK.Requests.Body
{
    public class TidalTrackFavoriteBody : TidalEmptyBody
    {
        [JsonProperty("trackId")]
        public string TrackId { get; set; }
    }
}