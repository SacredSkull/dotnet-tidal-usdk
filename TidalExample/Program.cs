using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
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
            var albumCover = tidalConnection.GetCoverUrl(trackInfo.Album.Cover);

            var artistId = trackInfo.Artists.First().Id;
            var artistVideos = await tidalConnection.AsyncGetArtistVideos(artistId);
            var artist = await tidalConnection.AsyncGetArtist(artistId);
            var artistBio = await tidalConnection.AsyncGetArtistBio(artistId);
            var artistTopTen = await tidalConnection.AsyncGetArtistTopTracks(artistId);
            var artistAlbums = await tidalConnection.AsyncGetArtistAlbums(artistId);
            var artistSimilar = await tidalConnection.AsyncGetSimilarArtists(artistId);

            var playlistSearch = await tidalConnection.AsyncSearch(
                "Dream Theater",
                new[]
                {
                    TidalQueryTypes.Playlists
                },
                1);
            var playlistId = playlistSearch.Playlists.First().Id;
            var playlist = await tidalConnection.AsyncGetPlaylist(playlistId);
            var playlistTracks = await tidalConnection.AsyncGetPlaylistTracks(playlistId);

            var myPlaylists = await tidalConnection.AsyncGetMyPlaylists();
            var myPlaylistId = myPlaylists.Items.First().Item.Id;
            var playlistRecommendations = await tidalConnection.AsyncGetPlaylistRecommendations(myPlaylistId);

            var albumSearch = await tidalConnection.AsyncSearch(
                "Systematic Chaos",
                new[]
                {
                    TidalQueryTypes.Albums
                },
                1);
            var albumId = albumSearch.Albums.First().Id;
            var album = await tidalConnection.AsyncGetAlbum(albumId);
            var albumTracks = await tidalConnection.AsyncGetAlbumTracks(albumId);

            var videoId = artistVideos.Items.First().Id;
            var video = await tidalConnection.AsyncGetVideo(videoId);
            var userId = tidalConnection.GetCurrentUserId();

            var favouriteArtists = await tidalConnection.AsyncGetMyFavouriteArtists();
            var favouriteAlbums = await tidalConnection.AsyncGetMyFavouriteAlbums();
            var favouriteTracks = await tidalConnection.AsyncGetMyFavouriteTracks(1951, null, 0, TidalOrderingEnum.DATE, TidalOrderingDirectionEnum.Ascending);
            var favouriteVideos = await tidalConnection.AsyncGetMyFavouriteVideos();

            Console.WriteLine($"Your user ID is {userId}");
            Console.WriteLine($"Enter request ({userId}): ");
            var input = Console.ReadLine();

            while (input != "exit")
            {
                Console.WriteLine();

                try
                {
                    var result = await tidalConnection.AsyncDebugQueryAPI(input);
                    Console.WriteLine(await result.Content.ReadAsStringAsync());
                }
                catch (FlurlHttpException e)
                {
                    Console.WriteLine($"<That request failed with status {e.Message}>");
                    Console.WriteLine(await e.GetResponseStringAsync());
                }

                Console.WriteLine();
                Console.WriteLine($"Enter request ({userId}): ");
                input = Console.ReadLine();
            }
        }
    }
}