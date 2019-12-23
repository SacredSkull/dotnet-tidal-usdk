using System;
using Newtonsoft.Json;

namespace TidalUSDK.Responses
{
    public class TidalFavouriteResponse<T>
    {
        [JsonProperty("created")]
        public virtual DateTimeOffset? FavouritedDate { get; set; }

        [JsonProperty("item")]
        public virtual T Item { get; set; }
    }
}