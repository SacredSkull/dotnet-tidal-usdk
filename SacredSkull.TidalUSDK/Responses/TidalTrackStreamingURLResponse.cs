using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SacredSkull.TidalUSDK.Enums;

namespace SacredSkull.TidalUSDK.Responses
{
    public class TidalTrackStreamingURLResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("trackId")]
        public string TrackId { get; set; }

        [JsonProperty("playTimeLeftInMinutes")]
        public int PlaytimeLeftInMinutes { get; set; }

        [JsonProperty("soundQuality")]
        public TidalStreamingQualityEnum SoundQuality { get; set; }

        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }

        [JsonProperty("codec")]
        public string Codec { get; set; }
    }
}