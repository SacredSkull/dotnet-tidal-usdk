using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TidalUSDK.Responses
{
    public class TidalPaginatedResponse<T>
    {
        [JsonProperty("limit")]
        public virtual int Limit { get; set; }

        [JsonProperty("offset")]
        public virtual int Offset { get; set; }

        [JsonProperty("totalNumberOfItems")]
        public virtual int TotalNumber { get; set; }

        [JsonProperty("items")]
        public virtual IEnumerable<T> Items { get; set; }

        [JsonIgnore]
        public bool HasResults => Items.Any();
    }
}