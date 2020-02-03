using NUnit.Framework;
using TidalUSDK.Extensions;

namespace TidalTests
{
    public class StringExtensionsTest
    {
        [Test]
        public void PathSegmentTest()
        {
            var result = StringExtensions.JoinPathSegments("should", "be", "joined");

            Assert.False(result.StartsWith('/'));
            Assert.True(result.Substring(6).StartsWith('/'));
            Assert.True(result.Substring(9).StartsWith('/'));
            Assert.False(result.EndsWith('/'));
        }

        [Test]
        public void PathSegmentWithOneSlashTest()
        {
            var result = StringExtensions.JoinPathSegments("should/", "be", "joined");

            Assert.False(result.StartsWith('/'));
            Assert.AreEqual('/', result[6]);
            Assert.AreNotEqual('/', result[7]);
            Assert.AreEqual('/', result[9]);
            Assert.AreNotEqual('/', result[10]);
            Assert.False(result.EndsWith('/'));
        }

        [Test]
        public void PathSegmentWithSlashesTest()
        {
            var result = StringExtensions.JoinPathSegments("should/", "/be", "joined");

            Assert.False(result.StartsWith('/'));
            Assert.AreEqual('/', result[6]);
            Assert.AreNotEqual('/', result[7]);
            Assert.AreEqual('/', result[9]);
            Assert.AreNotEqual('/', result[10]);
            Assert.False(result.EndsWith('/'));
        }
    }
}