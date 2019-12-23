using System.Collections.Generic;
using Newtonsoft.Json;
using TidalUSDK.Converters;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests
{
    public class TidalSearchRequest : TidalPaginatedRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("types")]
        [JsonConverter(typeof(CommaSeparatedEnumDeserializer))]
        public IEnumerable<TidalQueryTypes> Types { get; set; }
    }
}