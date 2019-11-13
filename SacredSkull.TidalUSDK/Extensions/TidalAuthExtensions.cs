using System.Collections.Generic;
using Flurl;
using Flurl.Http;
using SacredSkull.TidalUSDK.Constants;

namespace SacredSkull.TidalUSDK.Extensions
{
    public static class TidalAuthExtensions
    {
        public static IFlurlRequest AttachTidalAuth(this Url url, string sessionId)
        {
            return url.WithHeaders(new Dictionary<string, string>
            {
                {TidalHeaders.Origin, TidalUrls.Origin.ToString()},
                {TidalHeaders.SessionId, sessionId}
            });
        }
    }
}