using Newtonsoft.Json;

namespace SacredSkull.TidalUSDK.Requests
{
    public class TidalBaseRequest : TidalRequest
    {
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        public override void SetDefaults(string countryCode)
        {
            base.SetDefaults(countryCode);

            if (Limit < 1)
            {
                Limit = 999;
            }
        }
    }
}