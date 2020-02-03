namespace TidalTests.Enums
{
    public static class TestJSONFileNames
    {
        //
        // True to life, populated data
        //

        public static string TopAlbum => "TopAlbum";
        public static string TopArtist => "TopArtist";
        public static string TopTrack => "TopTrack";
        public static string TopPlaylist => "TopPlaylist";
        public static string TopVideo => "TopVideo";
        public static string Track => "Track";
        public static string Album => "Album";
        public static string Artist => "Artist";
        public static string ArtistBio => "ArtistBio";
        public static string ArtistVideos => "ArtistVideos";
        public static string AlbumTracks => "AlbumTracks";
        public static string Search => "Search";

        //
        // Valid, but empty data
        //

        public static string TopAlbumEmpty => "TopAlbumEmpty";
        public static string TopArtistEmpty => "TopArtistEmpty";
        public static string TopTrackEmpty => "TopTrackEmpty";
        public static string TopPlaylistEmpty => "TopPlaylistEmpty";
        public static string TopVideoEmpty => "TopVideoEmpty";
        public static string SearchEmpty => "SearchEmpty";

        //
        // Invalid data
        //
        public static string TopMissingType => "TopMissingType";
        public static string TopMissingValue => "TopMissingValue";
        public static string TopUnknownType => "TopUnknownType";
    }
}