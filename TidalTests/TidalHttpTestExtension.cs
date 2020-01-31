using System.Collections.Generic;
using System.IO;
using Flurl.Http.Testing;
using NUnit.Framework;

namespace TidalTests
{
    public static class TidalHttpTestExtension
    {
        private const string BaseDirectory = "Data";

        public static HttpTest RespondWithJsonString(
            this HttpTest httpTest,
            string jsonString,
            int statusCode = 200,
            Dictionary<object, object> headers = null)
        {
            if (headers == null)
                headers = new Dictionary<object, object>();

            headers.Add("Content-Type", "application/json");

            return httpTest.RespondWith(jsonString, statusCode, headers);
        }

        public static HttpTest RespondWithJsonStub(
            this HttpTest httpTest,
            string fileName,
            int statusCode = 200,
            Dictionary<object, object> headers = null)
        {
            return httpTest.RespondWithJsonString(GetJSONData(fileName), statusCode, headers);
        }

        private static string GetJSONData(string fileName)
        {
            if (!fileName.EndsWith(".json"))
                fileName += ".json";

            var path = Path.Join(TestContext.CurrentContext.TestDirectory, BaseDirectory, fileName);
            return File.ReadAllText(path);
        }
    }
}