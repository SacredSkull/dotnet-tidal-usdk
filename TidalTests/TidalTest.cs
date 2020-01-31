using System;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using NUnit.Framework;
using TidalUSDK;

namespace TidalTests
{
    public class TidalTest : BaseHttpTest
    {
        protected const string TokenHeader = "X-Tidal-Token";

        [Test]
        public void TestInvalidCredentials()
        {
            // empty username as string
            Client = new TidalClient("", "");
            Assert.ThrowsAsync<ArgumentException>(async () => await Client.ForceConnectAsync());

            // empty password as string
            Client = new TidalClient("hello", "");
            Assert.ThrowsAsync<ArgumentException>(async () => await Client.ForceConnectAsync());

            // empty username as SecureString
            Client = new TidalClient(new SecureString(), new SecureString());
            Assert.ThrowsAsync<ArgumentException>(async () => await Client.ForceConnectAsync());

            // empty password as SecureString
            var secureUsername = new SecureString();
            secureUsername.AppendChar('h');
            secureUsername.AppendChar('i');
            secureUsername.AppendChar('t');
            secureUsername.AppendChar('h');
            secureUsername.AppendChar('e');
            secureUsername.AppendChar('r');
            secureUsername.AppendChar('e');
            Client = new TidalClient(secureUsername, new SecureString());
            Assert.ThrowsAsync<ArgumentException>(async () => await Client.ForceConnectAsync());
        }

        [Test]
        public async Task TestLogin()
        {
            await Client.ForceConnectAsync();

            HttpTest
                .ShouldHaveCalled("*/login/username")
                .WithHeader(TokenHeader)
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.True(Client.IsConnected);
        }
    }
}