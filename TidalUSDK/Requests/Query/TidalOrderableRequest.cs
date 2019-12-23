using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests
{
    public class TidalOrderableRequest : TidalPaginatedRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("order")]
        public TidalOrderingEnum Order { get; set; }

        [JsonProperty("orderDirection")]
        public TidalOrderingDirectionEnum OrderDirection { get; set; }

        public override void SetDefaults(string countryCode)
        {
            base.SetDefaults(countryCode);

            if (this.OrderDirection == null)
            {
                this.OrderDirection = TidalOrderingDirectionEnum.Ascending;
            }
        }
    }
}