using System;
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
        public async Task<TidalPlaylist> GetPlaylistAsync(
            Guid playlistId, string countryCode = null, int limit = 999, int offset = 0)
        {
            var req = new TidalPaginatedRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Playlists, playlistId);
            var result = await QueryAPIAsync(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalPlaylist>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving this playlist ({playlistId}) does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        /// Get track recommendations based on a given playlist
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="countryCode"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<TidalPlaylistRecommendationsResponse> GetPlaylistRecommendationsAsync(
            Guid playlistId, string countryCode = null, int limit = 50, int offset = 0)
        {
            var req = new TidalPaginatedRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Playlists, playlistId, TidalUrls.PlaylistRecommendations);
            var result = await QueryAPIAsync(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalPlaylistRecommendationsResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving this playlist's ({playlistId}) recommendations does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalPlaylistTracksResponse> GetPlaylistTracksAsync(
            Guid playlistId, string countryCode = null, int limit = 999, TidalFilterTypes[] filterTypes = null, int offset = 0)
        {
            var req = new TidalFilterableRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset,
                Filters = filterTypes
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Playlists, playlistId, TidalUrls.Tracks);
            var result = await QueryAPIAsync(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalPlaylistTracksResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving this playlist's ({playlistId}) tracks does not appear to be valid. {e.Message}");
            }
        }
    }
}