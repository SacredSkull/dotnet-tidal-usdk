using Newtonsoft.Json;
using TidalUSDK.Entities;
using TidalUSDK.Enums;

namespace TidalUSDK.Responses
{
    public class TidalPlaylistFavourite : TidalFavouriteResponse<TidalPlaylist>
    {
        [JsonProperty("playlist")]
        public override TidalPlaylist Item { get; set; }

        [JsonProperty("type")]
        public virtual TidalPlaylistFavouriteType Type { get; set; }
    }
}