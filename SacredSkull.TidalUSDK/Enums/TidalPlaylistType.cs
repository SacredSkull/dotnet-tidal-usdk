using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SacredSkull.TidalUSDK.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TidalPlaylistType
    {
        ARTIST,
        EDITORIAL,
        USER
    }
}