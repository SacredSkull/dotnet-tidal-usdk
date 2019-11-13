using System;

namespace SacredSkull.TidalUSDK.Constants
{
    public class TidalUrls
    {
        /// <summary>
        ///     Base address.
        /// </summary>
        public static readonly Uri BaseAPI = new Uri("https://api.tidalhifi.com/v1/");

        public static readonly TidalUrls Origin = new TidalUrls("http://listen.tidal.com");

        public static readonly TidalUrls Login = new TidalUrls("/login/username");
        public static readonly TidalUrls Search = new TidalUrls("/search");

        // Artist related URL segments
        public static readonly TidalUrls Artists = new TidalUrls("/artists");
        public static readonly TidalUrls Bio = new TidalUrls("/bio");
        public static readonly TidalUrls TopTracks = new TidalUrls("/toptracks");
        public static readonly TidalUrls Videos = new TidalUrls("/videos");

        // Track related URL segments
        public static readonly TidalUrls Tracks = new TidalUrls("/tracks");
        public static readonly TidalUrls TrackStreamingURL = new TidalUrls("/streamUrl");
        public static readonly TidalUrls OfflineStreamingURL = new TidalUrls("/offlineUrl");


        private TidalUrls(string val)
        {
            Value = val;
        }

        private string Value { get; }

        public override string ToString()
        {
            return Value.TrimStart('/');
        }
    }
}