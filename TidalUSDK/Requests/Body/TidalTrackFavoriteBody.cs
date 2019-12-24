using Newtonsoft.Json;

namespace TidalUSDK.Requests.Body
{
    public class TidalTrackFavoriteBody : TidalEmptyBody
    {
        [JsonProperty("trackIds")]
        public string TrackId { get; set; }
    }
}