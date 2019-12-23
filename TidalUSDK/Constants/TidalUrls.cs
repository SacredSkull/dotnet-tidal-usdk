using System;

namespace TidalUSDK.Constants
{
    public class TidalUrls
    {
        /// <summary>
        ///     Base address.
        /// </summary>
        public static readonly Uri BaseAPI = new Uri("https://api.tidalhifi.com/v1/");
        public static readonly Uri ResourceRoot = new Uri("https://resources.tidal.com");
        public static readonly TidalUrls Origin = new TidalUrls("http://listen.tidal.com");

        // Login/session/me related URL segments
        public static readonly TidalUrls Login = new TidalUrls("/login/username");
        public static readonly TidalUrls Search = new TidalUrls("/search");
        public static readonly TidalUrls Session = new TidalUrls("/sessions");

        // Artist related URL segments
        public static readonly TidalUrls Artists = new TidalUrls("/artists");
        public static readonly TidalUrls Bio = new TidalUrls("/bio");
        public static readonly TidalUrls TopTracks = new TidalUrls("/toptracks");
        public static readonly TidalUrls Videos = new TidalUrls("/videos");
        public static readonly TidalUrls Similar = new TidalUrls("/similar");

        // Album related URL segments
        public static readonly TidalUrls Albums = new TidalUrls("/albums");

        // Track related URL segments
        public static readonly TidalUrls Tracks = new TidalUrls("/tracks");
        public static readonly TidalUrls TrackStreamingURL = new TidalUrls("/streamUrl");
        public static readonly TidalUrls OfflineStreamingURL = new TidalUrls("/offlineUrl");

        // Playlist related URL segments
        public static readonly TidalUrls Playlists = new TidalUrls("/playlists");
        public static readonly TidalUrls PlaylistRecommendations = new TidalUrls("/recommendations/items");
        public static readonly TidalUrls MyPlaylistsAndFavourited = new TidalUrls("/playlistsAndFavoritePlaylists");

        // Resource related URL segments (spot the dream theater album)
        public static readonly TidalUrls ImagesAndCovers = new TidalUrls("/images");

        // User related URL segments
        public static readonly TidalUrls Users = new TidalUrls("/users");
        public static readonly TidalUrls UserFavouriteArtists = new TidalUrls("/favorites/artists");
        public static readonly TidalUrls UserFavouriteAlbums = new TidalUrls("/favorites/albums");
        public static readonly TidalUrls UserFavouriteTracks = new TidalUrls("/favorites/tracks");
        public static readonly TidalUrls UserFavouriteVideos = new TidalUrls("/favorites/videos");

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