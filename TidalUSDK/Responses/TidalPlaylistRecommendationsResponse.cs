using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TidalUSDK.Entities;

namespace TidalUSDK.Responses
{
    public class TidalPlaylistRecommendationsResponse : TidalPaginatedResponse<TidalPlaylistRecommendationsResponse.TidalRecommendation>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TidalRecommendationType
        {
            TRACK,
            ALBUM,
            VIDEO
        }

        public class TidalRecommendation
        {
            [JsonProperty("item")]
            public TidalTrack Recommendation;

            [JsonProperty("type")]
            public TidalRecommendationType Type;
        }
    }
}