using Flurl.Http.Testing;
using NUnit.Framework;
using TidalUSDK;
using TidalUSDK.Responses;

namespace TidalTests
{
    public class BaseHttpTest : BaseTest
    {
        protected HttpTest HttpTest;
        protected TidalClient Client;

        [SetUp]
        protected void SetUp()
        {
            HttpTest = new HttpTest();
            SetupLogin();

            Client = new TidalClient("asd", "123");
        }

        [TearDown]
        protected void TearDown()
        {
            HttpTest.Dispose();
        }

        protected void SetupLogin()
        {
            HttpTest.RespondWithJson(new TidalLoginResponse()
            {
                CountryCode = "EU",
                SessionId = "12345678",
                StreamQuality = "HIGH",
                UserId = "10101010"
            });
        }
    }
}