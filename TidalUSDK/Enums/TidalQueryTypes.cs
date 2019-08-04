namespace TidalUSDK.Enums {
    public class TidalQueryTypes : CommaSeparatedStringEnum {
        private TidalQueryTypes(string val) : base(val) {}
        
        public static readonly TidalQueryTypes Artists = new TidalQueryTypes("ARTISTS");
        public static readonly TidalQueryTypes Albums = new TidalQueryTypes("ALBUMS");
        public static readonly TidalQueryTypes Tracks = new TidalQueryTypes("TRACKS");
        public static readonly TidalQueryTypes Videos = new TidalQueryTypes("VIDEOS");
        public static readonly TidalQueryTypes Playlists = new TidalQueryTypes("PLAYLISTS");
    }
}