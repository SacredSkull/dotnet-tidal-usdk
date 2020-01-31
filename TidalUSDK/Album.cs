using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TidalUSDK.Constants;
using TidalUSDK.Entities;
using TidalUSDK.Enums;
using TidalUSDK.Extensions;
using TidalUSDK.Requests;
using TidalUSDK.Responses;

namespace TidalUSDK
{
    public partial class TidalClient
    {
        public async Task<TidalAlbum> GetAlbumAsync(
            string albumId, string countryCode = null, int limit = 999, TidalFilterTypes[] filterTypes = null, int offset = 0)
        {
            var req = new TidalFilterableRequest
            {
                CountryCode = countryCode,
                Filters = filterTypes,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Albums, albumId);
            var result = await QueryAPIAsync(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalAlbum>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving album '{albumId}' does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalAlbumTracks> GetAlbumTracksAsync(
            string albumId, string countryCode = null, int limit = 999, TidalFilterTypes[] filterTypes = null, int offset = 0)
        {
            var req = new TidalFilterableRequest
            {
                CountryCode = countryCode,
                Filters = filterTypes,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Albums, albumId, TidalUrls.Tracks);
            var result = await QueryAPIAsync(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalAlbumTracks>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{albumId}' does not appear to be valid. {e.Message}");
            }
        }
    }
}