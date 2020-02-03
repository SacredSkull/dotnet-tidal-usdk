using System;
using Newtonsoft.Json;
using NUnit.Framework;
using TidalTests.Enums;
using TidalUSDK.Entities;
using TidalUSDK.Enums;

namespace TidalTests
{
    [TestFixture]
    public class TidalTopHitResultDeserializerTest : BaseTest
    {
        //
        // VALID DATA
        //

        private TidalTopHit AcquireTestTopResult(string fileName, TidalResultTypes type)
        {
            var testJson = this.GetJSONData(fileName);
            var topResult = JsonConvert.DeserializeObject<TidalTopHit>(testJson);

            Assert.AreEqual(type, topResult.Type);
            return topResult;
        }

        [Test]
        public void Test_Valid_TopAlbumResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopAlbum, TidalResultTypes.ALBUMS);

            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.NotNull(topResult.TopAlbum);
            Assert.AreEqual("minutes to midnight", topResult.TopAlbum.Title.ToLower());
        }

        [Test]
        public void Test_Valid_TopArtistResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopArtist, TidalResultTypes.ARTISTS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.NotNull(topResult.TopArtist);
            Assert.AreEqual("linkin park", topResult.TopArtist.Name.ToLower());
        }

        [Test]
        public void Test_Valid_TopPlaylistResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopPlaylist, TidalResultTypes.PLAYLISTS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.NotNull(topResult.TopPlaylist);
            Assert.AreEqual("dream theater essentials", topResult.TopPlaylist.Title.ToLower());
        }

        [Test]
        public void Test_Valid_TopTrackResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopTrack, TidalResultTypes.TRACKS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.NotNull(topResult.TopTrack);
            Assert.AreEqual("the count of tuscany", topResult.TopTrack.Title.ToLower());
        }

        [Test]
        public void Test_Valid_TopVideoResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopVideo, TidalResultTypes.VIDEOS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);

            Assert.NotNull(topResult.TopVideo);
            Assert.AreEqual("violence", topResult.TopVideo.Title.ToLower());
        }

        //
        // INVALID DATA
        //

        [Test]
        public void Test_Invalid_TopAlbumResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopAlbumEmpty, TidalResultTypes.ALBUMS);

            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.Null(topResult.TopAlbum);
        }

        [Test]
        public void Test_Invalid_TopArtistResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopArtistEmpty, TidalResultTypes.ARTISTS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.Null(topResult.TopArtist);
        }

        [Test]
        public void Test_Invalid_TopPlaylistResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopPlaylistEmpty, TidalResultTypes.PLAYLISTS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.Null(topResult.TopPlaylist);
        }

        [Test]
        public void Test_Invalid_TopTrackResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopTrackEmpty, TidalResultTypes.TRACKS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopVideo, Throws.InvalidOperationException);

            Assert.Null(topResult.TopTrack);
        }

        [Test]
        public void Test_Invalid_TopVideoResult()
        {
            var topResult = AcquireTestTopResult(TestJSONFileNames.TopVideoEmpty, TidalResultTypes.VIDEOS);

            Assert.That(() => topResult.TopAlbum, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopArtist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopPlaylist, Throws.InvalidOperationException);
            Assert.That(() => topResult.TopTrack, Throws.InvalidOperationException);

            Assert.Null(topResult.TopVideo);
        }

        [Test]
        public void Test_Invalid_MissingType()
        {
            Assert.Throws<JsonException>(() => AcquireTestTopResult(TestJSONFileNames.TopMissingType, TidalResultTypes.VIDEOS));
        }

        [Test]
        public void Test_Invalid_MissingValue()
        {
            Assert.Throws<JsonException>(() => AcquireTestTopResult(TestJSONFileNames.TopMissingValue, TidalResultTypes.VIDEOS));
        }

        [Test]
        public void Test_Invalid_UnknownType()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => AcquireTestTopResult(TestJSONFileNames.TopUnknownType, TidalResultTypes.VIDEOS));
        }
    }
}