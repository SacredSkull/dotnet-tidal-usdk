using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Entities;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Responses
{
    public class TidalPlaylistFavourite : TidalFavouriteResponse<TidalPlaylist>
    {
        [JsonProperty("playlist")]
        public override TidalPlaylist Item { get; set; }

        [JsonProperty("type")]
        public virtual TidalPlaylistFavouriteType Type { get; set; }
    }
}