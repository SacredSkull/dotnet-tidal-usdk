using Newtonsoft.Json;

namespace TidalUSDK.Requests
{
    public class TidalRequest : TidalEmptyRequest
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        public virtual void SetDefaults(string countryCode)
        {
            if (CountryCode == null)
            {
                CountryCode = countryCode;
            }
        }
    }
}