using System.Collections.Generic;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Deserializers;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Requests {
    public class TidalSearchRequest : TidalBaseRequest {
        [JsonProperty("query")]
        public string Query { get; set; }
        
        [JsonProperty("types")]
        [JsonConverter(typeof(CommaSeparatedEnumDeserializer))]
        public IEnumerable<TidalQueryTypes> Types { get; set; }
    }
}