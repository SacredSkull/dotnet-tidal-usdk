using System;

namespace TidalUSDK.Constants {
    public class TidalUrls {
        private TidalUrls(string val) {
            this.Value = val;
        }
        
        private string Value { get; set; }

        public override string ToString() {
            return this.Value.TrimStart('/');
        }

        /// <summary>
        /// Base address.
        /// </summary>
        public static readonly Uri BaseAPI = new Uri("https://api.tidalhifi.com/v1/");
        
        public static readonly TidalUrls Login = new TidalUrls("/login/username");
        public static readonly TidalUrls Search = new TidalUrls("/search");
        public static readonly TidalUrls Artists = new TidalUrls("/artists");
        public static readonly TidalUrls Bio = new TidalUrls("/bio");
        public static readonly TidalUrls TopTracks = new TidalUrls("/toptracks");
        public static readonly TidalUrls Origin = new TidalUrls("http://listen.tidal.com");
    }
}