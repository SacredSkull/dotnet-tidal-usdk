using System.Collections.Generic;
using Newtonsoft.Json;
using TidalUSDK.Enums;
using TidalUSDK.Serializers;

namespace TidalUSDK.Requests {
    public class TidalSearchRequest : TidalBaseRequest {
        [JsonProperty("query")]
        public string Query { get; set; }
        
        [JsonProperty("types")]
        [JsonConverter(typeof(CommaSeparatedEnumSerializer))]
        public IEnumerable<TidalQueryTypes> Types { get; set; }
    }
}