using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TidalUSDK.Deserializers;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests {
    public class TidalArtistRequest : TidalBaseRequest {
        [JsonConverter(typeof(CommaSeparatedEnumDeserializer))]
        [JsonProperty("filter")]
        public IEnumerable<TidalFilterTypes> Filters { get; set; }

        public override void SetDefaults(string countryCode) {
            base.SetDefaults(countryCode);

            if (this.Filters == null || !this.Filters.Any()) {
                Filters = new [] { TidalFilterTypes.All };
            }
        }
    }
}