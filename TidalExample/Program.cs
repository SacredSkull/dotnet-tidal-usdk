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

            var search = await tidalClient.SearchAsync(
                "Thrill Me Riva Starr",
                new[]
                {
                    TidalQueryTypes.Tracks
                },
                5);
            var trackId = search.Tracks.Items.Last(track => track.Artists.Any(trackArtist => trackArtist.Type == "MAIN")).Id;
            var trackInfo = await tidalClient.GetTrackAsync(trackId);

            await tidalClient.AddTrackToMyLibraryAsync(trackInfo.Id);

            /* Kind of distracting for TIDAL to pause your music while coding (since you're "playing" on more than one device at a time) */
            //var trackStreamingURL = await tidalConnection.AsyncGetTrackStreamingURL(trackId, TidalStreamingQualityEnum.HIGH);
            //var trackOfflineStreamingURL = await tidalConnection.AsyncGetTrackOfflineStreamingURL(trackId, TidalStreamingQualityEnum.HIGH);
            var albumCover = tidalClient.GetCoverUrl(trackInfo.Album.Cover);

            var artistId = trackInfo.Artists.First().Id;
            var artistVideos = await tidalClient.GetArtistVideosAsync(artistId);
            var artist = await tidalClient.GetArtistAsync(artistId);
            var artistBio = await tidalClient.GetArtistBioAsync(artistId);
            var artistTopTen = await tidalClient.GetArtistTopTracksAsync(artistId);
            var artistAlbums = await tidalClient.GetArtistAlbumsAsync(artistId);
            var artistSimilar = await tidalClient.GetSimilarArtistsAsync(artistId);

            var playlistSearch = await tidalClient.SearchAsync(
                "Dream Theater",
                new[]
                {
                    TidalQueryTypes.Playlists
                },
                1);
            var playlistId = playlistSearch.Playlists.Items.First().Id;
            var playlist = await tidalClient.GetPlaylistAsync(playlistId);

            var myPlaylists = await tidalClient.GetMyPlaylistsAsync();
            var myPlaylistId = myPlaylists.Items.First().Item.Id;
            var playlistRecommendations = await tidalClient.GetPlaylistRecommendationsAsync(myPlaylistId);
            var playlistTracks = await tidalClient.GetPlaylistTracksAsync(myPlaylistId);

            var albumSearch = await tidalClient.SearchAsync(
                "Systematic Chaos",
                new[]
                {
                    TidalQueryTypes.Albums
                },
                1);
            var albumId = albumSearch.Albums.Items.First().Id;
            var album = await tidalClient.GetAlbumAsync(albumId);
            var albumTracks = await tidalClient.GetAlbumTracksAsync(albumId);

            var videoSearch = await tidalClient.SearchAsync(
                "Love",
                new[]
                {
                    TidalQueryTypes.Videos
                },
                1);
            var videoId = videoSearch.Videos.Items.First().Id;
            var video = await tidalClient.GetVideoAsync(videoId);
            var userId = tidalClient.GetCurrentUserId();

            var favouriteArtists = await tidalClient.GetMyFavouriteArtistsAsync();
            var favouriteAlbums = await tidalClient.GetMyFavouriteAlbumsAsync();
            var favouriteTracks = await tidalClient.GetMyFavouriteTracksAsync(1951, null, 0, TidalOrderingEnum.DATE, TidalOrderingDirectionEnum.Ascending);
            var favouriteVideos = await tidalClient.GetMyFavouriteVideosAsync();

            var favTrack = favouriteTracks.Items.First().Item;
            Console.WriteLine($"Deleting {favTrack.Title} by {favTrack.Artists.First().Name}");
            await tidalClient.GetTrackStreamingURLAsync(favTrack.Id, TidalStreamingQualityEnum.HIGH);
            await tidalClient.RemoveTrackFromMyLibraryAsync(favTrack.Id);

            Console.WriteLine($"...And now putting it back again.");
            await tidalClient.AddTrackToMyLibraryAsync(favTrack.Id);

            Console.WriteLine($"Your user ID is {userId}");
            Console.WriteLine($"Enter request ({userId}): ");
            var input = Console.ReadLine();

            while (input != "exit")
            {
                Console.WriteLine();

                try
                {
                    var result = await tidalClient.DebugQueryAPIAsync(input);
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

            await tidalClient.AddTrackToMyLibraryAsync(favTrack.Id);
        }
    }
}