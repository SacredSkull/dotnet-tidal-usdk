using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TidalUSDK.Enums;
using TidalUSDK.Serializers;

namespace TidalUSDK.Requests {
    public class TidalArtistRequest : TidalBaseRequest {
        [JsonConverter(typeof(CommaSeparatedEnumSerializer))]
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