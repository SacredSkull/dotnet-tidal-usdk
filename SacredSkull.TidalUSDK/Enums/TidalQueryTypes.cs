namespace SacredSkull.TidalUSDK.Enums
{
    public class TidalQueryTypes : CommaSeparatedStringEnum
    {
        public static readonly TidalQueryTypes Artists = new TidalQueryTypes(TidalResultTypes.ARTISTS.ToString());
        public static readonly TidalQueryTypes Albums = new TidalQueryTypes(TidalResultTypes.ALBUMS.ToString());
        public static readonly TidalQueryTypes Tracks = new TidalQueryTypes(TidalResultTypes.TRACKS.ToString());
        public static readonly TidalQueryTypes Videos = new TidalQueryTypes(TidalResultTypes.VIDEOS.ToString());
        public static readonly TidalQueryTypes Playlists = new TidalQueryTypes(TidalResultTypes.PLAYLISTS.ToString());

        private TidalQueryTypes(string val) : base(val)
        {
        }
    }
}