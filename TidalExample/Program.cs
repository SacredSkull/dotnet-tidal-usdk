using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SacredSkull.TidalUSDK;
using SacredSkull.TidalUSDK.Enums;

namespace TidalTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string strConf = "";
            try
            {
                strConf = File.ReadAllText(Path.Join(Environment.CurrentDirectory, "tidal-auth.json"),
                    Encoding.UTF8);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("You must provide TIDAL auth information in the tidal-auth.json file.");
                return;
            }

            TidalConnection tidalConnection;

            {
                dynamic conf = JObject.Parse(strConf);

                if (string.IsNullOrWhiteSpace(conf.username.ToString()) ||
                    string.IsNullOrWhiteSpace(conf.password.ToString()))
                {
                    Console.WriteLine("The config for TIDAL auth does not appear to be correct.");
                    return;
                }

                tidalConnection = new TidalConnection(conf.username.ToString(), conf.password.ToString());
            }

            var search = await tidalConnection.AsyncSearch(
                "In the presence of enemies, pt. 1",
                new[]
                {
                    TidalQueryTypes.Tracks
                },
                5);
            var trackId = search.Tracks.Last(track => track.Artists.Any(trackArtist => trackArtist.Type == "MAIN")).Id;
            var trackInfo = await tidalConnection.AsyncGetTrack(trackId);

            /* Kind of distracting for TIDAL to pause your music while coding (since you're "playing" on more than one device at a time) */
            //var trackStreamingURL = await tidalConnection.AsyncGetTrackStreamingURL(trackId, TidalStreamingQualityEnum.HIGH);
            //var trackOfflineStreamingURL = await tidalConnection.AsyncGetTrackOfflineStreamingURL(trackId, TidalStreamingQualityEnum.HIGH);

            var artistId = trackInfo.Artists.First().Id;
            var artistVideos = await tidalConnection.AsyncGetArtistVideos(artistId);
            var artist = await tidalConnection.AsyncGetArtist(artistId, new[] { TidalFilterTypes.All });
            var artistBio = await tidalConnection.AsyncGetArtistBio(artistId);
            var artistTopTen = await tidalConnection.AsyncGetArtistTopTracks(artistId, new[] { TidalFilterTypes.All });
        }
    }
}