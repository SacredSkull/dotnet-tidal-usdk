using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TidalUSDK.Entities
{
    public class TidalTrack
    {
        [JsonIgnore]
        private decimal _peak;

        [JsonIgnore]
        private decimal _replayGain;

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///     ReplayGain is a track metric used for consistent volume leveling of tracks that are not on the same album (e.g.
        ///     shuffling).
        ///     Not typically applied if each track in a queue is from the same album to respect the album's mastering, but some
        ///     clients allow you to override this.
        /// </summary>
        /// <remarks>
        ///     Json.NET does something very odd with decimals, so we have to round to TIDAL's base decimal accuracy, hence the
        ///     backing field...
        /// </remarks>
        [JsonProperty("replayGain")]
        public decimal ReplayGain
        {
            get => _replayGain;
            set => _replayGain = decimal.Round(value, 2);
        }

        /// <summary>
        ///     Max peak (volume) of song, useful alongside <see cref="ReplayGain" /> information.
        /// </summary>
        [JsonProperty("peak")]
        public decimal Peak
        {
            get => _peak;
            set => _peak = decimal.Round(value, 6);
        }

        [JsonProperty("streamReady")]
        public bool IsReadyToStream { get; set; }

        [JsonProperty("streamStartDate")]
        public DateTimeOffset? StreamingAvailableSince { get; set; }

        [JsonProperty("allowStreaming")]
        public bool StreamingAllowed { get; set; }

        [JsonProperty("premiumStreamingOnly")]
        public bool PremiumStreamingOnly { get; set; }

        [JsonProperty("trackNumber")]
        public int TrackNumber { get; set; }

        [JsonProperty("volumeNumber")]
        public int VolumeNumber { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("isrc")]
        public string ISRC { get; set; }

        [JsonProperty("editable")]
        public bool Editable { get; set; }

        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        //[JsonProperty("audioQuality")]
        //public TidalQuality Quality { get; set; }

        //[JsonProperty("surroundTypes")]
        //public IEnumerable<TidalSurround> SurroundTypes { get; set; }

        [JsonProperty("artists")]
        public IEnumerable<TidalNestedArtist> Artists { get; set; }

        [JsonProperty("album")]
        public TidalNestedAlbum Album { get; set; }
    }
}