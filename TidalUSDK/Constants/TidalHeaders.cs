namespace TidalUSDK.Constants {
    public static class TidalHeaders {
        public static string Username = "username";
        public static string Password = "password";
        
        /// <summary>
        /// This token identifies the client archetype - e.g. the Android App, or the iOS app
        /// On TIDAL's end, this affects the return type of streams (and perhaps other features).
        /// The Android/iOS streams are unencrypted, the desktop ones are encrypted.
        /// </summary>
        public static string ClientTypeToken = "X-Tidal-Token";
        
        /// <summary>
        /// This seems to be an individual client tracking key.
        /// </summary>
        public static string ClientUniqueKey = "clientUniqueKey";

        /// <summary>
        /// Standard ol' session ID.
        /// </summary>
        public static string SessionId = "X-Tidal-SessionId";

        /// <summary>
        /// Plain ol' origin header.
        /// </summary>
        public static string Origin = "Origin";
    }
}