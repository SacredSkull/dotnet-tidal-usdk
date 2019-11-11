using System.Collections.Generic;
using Newtonsoft.Json;
using TidalUSDK.Enums;

namespace TidalUSDK.Requests {
    public abstract class TidalBaseRequest {
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit { get; set; }
        
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        public virtual void SetDefaults(string countryCode) {
            if (this.CountryCode == null) {
                this.CountryCode = countryCode;
            }

            if (this.Limit < 1) {
                this.Limit = 999;
            }
        }
    }
}