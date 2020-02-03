using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using TidalTests.Enums;

namespace TidalTests
{
    public class ArtistTest : BaseHttpTest
    {
        [Test]
        public async Task GetArtistTest()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.Artist);

            var artist = await Client.GetArtistAsync("1234");

            Assert.NotNull(artist);
            Assert.AreEqual("Junior Jack", artist.Name);
            Assert.AreEqual(22, artist.Popularity);
        }

        [Test]
        public async Task GetArtistVideosTest()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.ArtistVideos);

            var artistVideos = await Client.GetArtistVideosAsync("1234");

            Assert.NotNull(artistVideos);
            Assert.AreEqual(999, artistVideos.Limit);
            Assert.AreEqual(artistVideos.TotalNumber, artistVideos.Items.Count());
            Assert.AreEqual("Barstool Warrior", artistVideos.Items.First().Title);
        }

        [Test]
        public async Task GetArtistBioTest()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.ArtistBio);

            var artistBio = await Client.GetArtistBioAsync("1234");

            Assert.NotNull(artistBio);
            Assert.True(artistBio.Text.StartsWith("Vito"));
        }


        [Test]
        public void GetInvalidArtistVideosTest()
        {
            HttpTest.RespondWithJsonString("asd{}");
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.GetArtistVideosAsync("1234"));
        }

        [Test]
        public void GetInvalidArtistTest()
        {
            HttpTest.RespondWithJsonString("asd{}");
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.GetArtistAsync("1234"));
        }

        [Test]
        public void GetInvalidArtistBioTest()
        {
            HttpTest.RespondWithJsonString("asd{}");
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.GetArtistBioAsync("1234"));
        }
    }
}