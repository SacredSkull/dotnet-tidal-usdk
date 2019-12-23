using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TidalUSDK.Converters;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests
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