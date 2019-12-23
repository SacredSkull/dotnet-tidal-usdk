using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TidalUSDK.Constants;
using TidalUSDK.Entities;
using TidalUSDK.Extensions;
using TidalUSDK.Requests;

namespace TidalUSDK
{
    public partial class TidalClient
    {
        /// <summary>
        ///     Get video
        /// </summary>
        /// <param name="videoId">The artist's TIDAL ID</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalVideo> AsyncGetVideo(string videoId, string countryCode = null)
        {
            var req = new TidalRequest
            {
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Videos, videoId);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalVideo>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving video '{videoId}' does not appear to be valid. {e.Message}");
            }
        }
    }
}