using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Constants;
using SacredSkull.TidalUSDK.Entities;
using SacredSkull.TidalUSDK.Enums;
using SacredSkull.TidalUSDK.Extensions;
using SacredSkull.TidalUSDK.Requests;
using SacredSkull.TidalUSDK.Responses;

namespace SacredSkull.TidalUSDK
{
    public partial class TidalClient
    {
        public async Task<TidalAlbum> AsyncGetAlbum(
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
            var result = await AsyncQueryAPI(url, req);
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

        public async Task<TidalAlbumTracks> AsyncGetAlbumTracks(
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
            var result = await AsyncQueryAPI(url, req);
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