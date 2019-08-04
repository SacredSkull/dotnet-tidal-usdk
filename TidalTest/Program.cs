using System.Threading.Tasks;
using TidalUSDK;
using TidalUSDK.Enums;

namespace TidalTest {
    class Program {
        static async Task Main(string[] args) {
            TidalConnection tidalConnection = new TidalConnection("", "");

            var search = await tidalConnection.AsyncSearch("Dream", new []{ TidalQueryTypes.Artists, TidalQueryTypes.Tracks, TidalQueryTypes.Albums, TidalQueryTypes.Playlists, TidalQueryTypes.Videos }, 5);
            var artist = await tidalConnection.AsyncGetArtist("14670", new [] { TidalFilterTypes.All });
            var artistBio = await tidalConnection.AsyncGetArtistBio("14670");
            var artistTopTen = await tidalConnection.AsyncGetArtistTopTracks("14670", new [] { TidalFilterTypes.All });
        }
    }
}