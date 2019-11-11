using System.IO;
using NUnit.Framework;

namespace TidalTest
{
    public abstract class BaseTest
    {
        private const string BaseDirectory = "Data";

        public string GetJSONData(string fileName)
        {
            var path = Path.Join(TestContext.CurrentContext.TestDirectory, BaseDirectory, fileName + ".json");
            return File.ReadAllText(path);
        }
    }
}