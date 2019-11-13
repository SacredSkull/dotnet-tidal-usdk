using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Requests
{
    public class TidalRequest
    {
        [JsonProperty("countryCode")] public string CountryCode { get; set; }

        public virtual void SetDefaults(string countryCode)
        {
            if (CountryCode == null)
            {
                CountryCode = countryCode;
            }
        }
    }
}