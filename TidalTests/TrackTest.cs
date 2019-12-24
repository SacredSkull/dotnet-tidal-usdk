using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using TidalTest.Enums;
using TidalUSDK;
using TidalUSDK.Constants;
using TidalUSDK.Entities;

namespace TidalTests
{
    public class TidalTrackTest : BaseHttpTest
    {
        private const string TrackId = "68574405";

        [Test]
        public async Task GetTrackInfo()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.Track);

            var track = await Client.AsyncGetTrack(TrackId);

            HttpTest.ShouldHaveCalled($"*{TidalUrls.Tracks}*{TrackId}");
            Assert.AreEqual("in the presence of enemies, pt. 1", track.Title.ToLower());
        }

        [Test]
        public async Task GetInvalidTrackResponse()
        {
            HttpTest.RespondWithJsonString("/\\/");
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.AsyncGetTrack(TrackId));
        }
    }
}