using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using TidalTests.Enums;
using TidalUSDK.Enums;

namespace TidalTests
{
    public class AlbumTest : BaseHttpTest
    {
        [Test]
        public async Task GetAlbumTest()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.Album);

            var album = await Client.GetAlbumAsync("1234");

            Assert.NotNull(album);
            Assert.AreEqual("Systematic Chaos", album.Title);
            Assert.AreEqual(8, album.TrackCount);
            Assert.AreEqual(1, album.ArtistIds.Count());

            var mainArtist = album.MainArtist;
            Assert.AreEqual("Dream Theater", mainArtist.Name);
        }

        [Test]
        public void GetInvalidAlbumTest()
        {
            HttpTest.RespondWithJsonString("asd{}");
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.GetAlbumAsync("1234"));
        }

        [Test]
        public async Task GetAlbumTracksTest()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.AlbumTracks);

            var albumTracks = await Client.GetAlbumTracksAsync("1234");

            Assert.NotNull(albumTracks);
            Assert.True(albumTracks.HasResults);
            Assert.AreEqual(albumTracks.TotalNumber, albumTracks.Items.Count());
        }

        [Test]
        public void GetInvalidAlbumTracksTest()
        {
            HttpTest.RespondWithJsonString("asd{}");
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.GetAlbumTracksAsync("1234"));
        }
    }
}