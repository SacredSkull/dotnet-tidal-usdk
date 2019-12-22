using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Requests.Body
{
    public class TidalTrackFavoriteBody : TidalEmptyBody
    {
        [JsonProperty("trackId")]
        public string TrackId { get; set; }
    }
}