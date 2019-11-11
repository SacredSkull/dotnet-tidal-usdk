using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TidalUSDK;
using TidalUSDK.Enums;

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
                "In the presence of enemies",
                new[]
                {
                    TidalQueryTypes.Artists,
                    TidalQueryTypes.Tracks, TidalQueryTypes.Albums,
                    TidalQueryTypes.Playlists, TidalQueryTypes.Videos
                },
                5);

            var artist = await tidalConnection.AsyncGetArtist("14670", new[] {TidalFilterTypes.All});
            var artistBio = await tidalConnection.AsyncGetArtistBio("14670");
            var artistTopTen = await tidalConnection.AsyncGetArtistTopTracks("14670", new[] {TidalFilterTypes.All});
        }
    }
}