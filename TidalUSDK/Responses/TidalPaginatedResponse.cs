using System.Collections.Generic;
using Newtonsoft.Json;

namespace TidalUSDK.Responses
{
    public abstract class TidalPaginatedResponse<T>
    {
        [JsonProperty("limit")]
        public virtual int Limit { get; set; }

        [JsonProperty("offset")]
        public virtual int Offset { get; set; }

        [JsonProperty("totalNumberOfItems")]
        public virtual int TotalNumber { get; set; }

        [JsonProperty("items")]
        public virtual IEnumerable<T> Items { get; set; }
    }
}