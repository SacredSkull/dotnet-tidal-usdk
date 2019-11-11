using System.Collections.Generic;
using Newtonsoft.Json;
using TidalUSDK.Deserializers;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests {
    public class TidalSearchRequest : TidalBaseRequest {
        [JsonProperty("query")]
        public string Query { get; set; }
        
        [JsonProperty("types")]
        [JsonConverter(typeof(CommaSeparatedEnumDeserializer))]
        public IEnumerable<TidalQueryTypes> Types { get; set; }
    }
}