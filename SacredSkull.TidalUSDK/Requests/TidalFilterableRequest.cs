using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Converters;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Requests
{
    public class TidalFilterableRequest : TidalPaginatedRequest
    {
        [JsonConverter(typeof(CommaSeparatedEnumDeserializer))]
        [JsonProperty("filter")]
        public IEnumerable<TidalFilterTypes> Filters { get; set; }

        public override void SetDefaults(string countryCode)
        {
            base.SetDefaults(countryCode);

            if (Filters == null || !Filters.Any())
            {
                Filters = new[] { TidalFilterTypes.All };
            }
        }
    }
}