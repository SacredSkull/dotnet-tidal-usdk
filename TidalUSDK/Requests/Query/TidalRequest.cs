using Newtonsoft.Json;
using TidalUSDK.Requests.Query;

namespace TidalUSDK.Requests
{
    public class TidalRequest : TidalEmptyRequest
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        public override void SetDefaults(string countryCode)
        {
            if (CountryCode == null)
            {
                CountryCode = countryCode;
            }
        }
    }
}