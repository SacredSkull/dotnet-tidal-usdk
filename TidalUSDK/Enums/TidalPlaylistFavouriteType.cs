using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TidalUSDK.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TidalPlaylistFavouriteType
    {
        USER_CREATED
    }
}