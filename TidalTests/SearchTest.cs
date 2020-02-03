using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TidalTests.Enums;
using TidalUSDK.Enums;

namespace TidalTests
{
    public class SearchTest : BaseHttpTest
    {
        [Test]
        public async Task GetSearchResults()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.Search);

            var searchResults = await Client.SearchAsync(
                "dream theater",
                new []
                {
                    TidalQueryTypes.Artists,
                    TidalQueryTypes.Tracks,
                    TidalQueryTypes.Albums,
                    TidalQueryTypes.Videos,
                    TidalQueryTypes.Playlists,
                });

            Assert.NotNull(searchResults);
            Assert.AreEqual(searchResults.Albums.TotalNumber, searchResults.Albums.Items.Count());
            Assert.AreEqual(searchResults.Artists.TotalNumber, searchResults.Artists.Items.Count());
            Assert.AreEqual(searchResults.Playlists.TotalNumber, searchResults.Playlists.Items.Count());
            Assert.AreEqual(searchResults.Tracks.TotalNumber, searchResults.Tracks.Items.Count());
            Assert.AreEqual(searchResults.Videos.TotalNumber, searchResults.Videos.Items.Count());
            Assert.AreEqual(TidalResultTypes.ARTISTS, searchResults.TopHit.Type);
        }

        [Test]
        public async Task TestEmptySearchResults()
        {
            HttpTest.RespondWithJsonStub(TestJSONFileNames.SearchEmpty);

            var searchResults = await Client.SearchAsync(
                "gfhfghgfhgfhgfgfhgf",
                new []
                {
                    TidalQueryTypes.Artists,
                    TidalQueryTypes.Tracks,
                    TidalQueryTypes.Albums,
                    TidalQueryTypes.Videos,
                    TidalQueryTypes.Playlists,
                });

            Assert.NotNull(searchResults);
            Assert.AreEqual(searchResults.Albums.TotalNumber, searchResults.Albums.Items.Count());
            Assert.AreEqual(searchResults.Artists.TotalNumber, searchResults.Artists.Items.Count());
            Assert.AreEqual(searchResults.Playlists.TotalNumber, searchResults.Playlists.Items.Count());
            Assert.AreEqual(searchResults.Tracks.TotalNumber, searchResults.Tracks.Items.Count());
            Assert.AreEqual(searchResults.Videos.TotalNumber, searchResults.Videos.Items.Count());
            Assert.Null(searchResults.TopHit);
            Assert.False(searchResults.HasResults);
        }
    }
}