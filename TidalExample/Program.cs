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

            TidalClient tidalClient;

            {
                dynamic conf = JObject.Parse(strConf);

                if (string.IsNullOrWhiteSpace(conf.username.ToString()) ||
                    string.IsNullOrWhiteSpace(conf.password.ToString()))
                {
                    Console.WriteLine("The config for TIDAL auth does not appear to be correct.");
                    return;
                }

                tidalClient = new TidalClient(conf.username.ToString(), conf.password.ToString());
            }

            var search = await tidalClient.AsyncSearch(
                "In the presence of enemies, pt. 1",
                new[]
                {
                    TidalQueryTypes.Tracks
                },
                5);
            var trackId = search.Tracks.Last(track => track.Artists.Any(trackArtist => trackArtist.Type == "MAIN")).Id;
            var trackInfo = await tidalClient.AsyncGetTrack(trackId);

            /* Kind of distracting for TIDAL to pause your music while coding (since you're "playing" on more than one device at a time) */
            //var trackStreamingURL = await tidalConnection.AsyncGetTrackStreamingURL(trackId, TidalStreamingQualityEnum.HIGH);
            //var trackOfflineStreamingURL = await tidalConnection.AsyncGetTrackOfflineStreamingURL(trackId, TidalStreamingQualityEnum.HIGH);
            var albumCover = tidalClient.GetCoverUrl(trackInfo.Album.Cover);

            var artistId = trackInfo.Artists.First().Id;
            var artistVideos = await tidalClient.AsyncGetArtistVideos(artistId);
            var artist = await tidalClient.AsyncGetArtist(artistId);
            var artistBio = await tidalClient.AsyncGetArtistBio(artistId);
            var artistTopTen = await tidalClient.AsyncGetArtistTopTracks(artistId);
            var artistAlbums = await tidalClient.AsyncGetArtistAlbums(artistId);
            var artistSimilar = await tidalClient.AsyncGetSimilarArtists(artistId);

            var playlistSearch = await tidalClient.AsyncSearch(
                "Dream Theater",
                new[]
                {
                    TidalQueryTypes.Playlists
                },
                1);
            var playlistId = playlistSearch.Playlists.First().Id;
            var playlist = await tidalClient.AsyncGetPlaylist(playlistId);
            var playlistTracks = await tidalClient.AsyncGetPlaylistTracks(playlistId);

            var myPlaylists = await tidalClient.AsyncGetMyPlaylists();
            var myPlaylistId = myPlaylists.Items.First().Item.Id;
            var playlistRecommendations = await tidalClient.AsyncGetPlaylistRecommendations(myPlaylistId);

            var albumSearch = await tidalClient.AsyncSearch(
                "Systematic Chaos",
                new[]
                {
                    TidalQueryTypes.Albums
                },
                1);
            var albumId = albumSearch.Albums.First().Id;
            var album = await tidalClient.AsyncGetAlbum(albumId);
            var albumTracks = await tidalClient.AsyncGetAlbumTracks(albumId);

            var videoId = artistVideos.Items.First().Id;
            var video = await tidalClient.AsyncGetVideo(videoId);
            var userId = tidalClient.GetCurrentUserId();

            var favouriteArtists = await tidalClient.AsyncGetMyFavouriteArtists();
            var favouriteAlbums = await tidalClient.AsyncGetMyFavouriteAlbums();
            var favouriteTracks = await tidalClient.AsyncGetMyFavouriteTracks(1951, null, 0, TidalOrderingEnum.DATE, TidalOrderingDirectionEnum.Ascending);
            var favouriteVideos = await tidalClient.AsyncGetMyFavouriteVideos();

            var favTrack = favouriteTracks.Items.First().Item;
            await tidalClient.AsyncRemoveTrackFromMyLibrary(favTrack.Id);
            Console.WriteLine($"Deleting {favTrack.Title} by {favTrack.Artists.First().Name}");

            await tidalClient.AsyncAddTrackToMyLibrary(favTrack.Id);
            Console.WriteLine($"...And now putting it back again.");

            Console.WriteLine($"Your user ID is {userId}");
            Console.WriteLine($"Enter request ({userId}): ");
            var input = Console.ReadLine();

            while (input != "exit")
            {
                Console.WriteLine();

                try
                {
                    var result = await tidalClient.AsyncDebugQueryAPI(input);
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

            await tidalClient.AsyncAddTrackToMyLibrary(favTrack.Id);
        }
    }
}