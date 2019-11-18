using System.Collections.Generic;
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
        /// <summary>
        ///     Get artist information, not including bio.
        ///     See also:
        ///     <seealso cref="AsyncGetArtistBio" />
        ///     <seealso cref="AsyncGetArtistTopTracks" />
        /// </summary>
        /// <param name="artistId">The artist's TIDAL ID</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="limit">Number of results to limit to</param>
        /// <param name="offset">Offset of results (NOT page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtist> AsyncGetArtist(
            string artistId, IEnumerable<TidalFilterTypes> filterType = null, int limit = 0, int offset = 0, string countryCode = null)
        {
            var req = new TidalArtistRequest
            {
                Filters = filterType,
                Offset = offset,
                Limit = limit,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtist>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{artistId}' does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Get artist videos
        /// </summary>
        /// <param name="artistId">The artist's TIDAL ID</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset of results (NOT page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtistVideosResponse> AsyncGetArtistVideos(
            string artistId, IEnumerable<TidalFilterTypes> filterType = null, int limit = 0, int offset = 0, string countryCode = null)
        {
            var req = new TidalFilterableRequest
            {
                Filters = filterType,
                Offset = offset,
                Limit = limit,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.Videos);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistVideosResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{artistId}' does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Get artist bio
        /// </summary>
        /// <param name="artistId">The artist (name or ID)</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtistBio> AsyncGetArtistBio(string artistId, string countryCode = null)
        {
            var req = new TidalRequest
            {
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.Bio);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistBio>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{artistId}' does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalArtistAlbumsResponse> AsyncGetArtistAlbums(
            string artistId, string countryCode = null, int limit = 999, TidalFilterTypes[] filterTypes = null, int offset = 0)
        {
            var req = new TidalFilterableRequest
            {
                CountryCode = countryCode,
                Filters = filterTypes,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.Albums);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistAlbumsResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist's '{artistId}' albums does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///    Get similar artists of a given artist
        /// </summary>
        /// <param name="artistId">Artist Id</param>
        /// <param name="countryCode">Country code</param>
        /// <param name="limit">Number of results to limit to</param>
        /// <param name="filterTypes">Tidal filter types</param>
        /// <param name="offset">Offset of results (i.e. if you requested 50 on the last "page" you need to offset 50)</param>
        /// <returns>TidalSimilarArtistsResponse</returns>
        /// <exception cref="HttpRequestException">If JSON is invalid</exception>
        /// <remarks>
        ///     This API call seems to have a max limit of 50, so it's set to that by default
        /// </remarks>
        public async Task<TidalSimilarArtistsResponse> AsyncGetSimilarArtists(
            string artistId, string countryCode = null, int limit = 50, TidalFilterTypes[] filterTypes = null, int offset = 0)
        {
            var req = new TidalFilterableRequest
            {
                CountryCode = countryCode,
                Filters = filterTypes,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.Similar);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalSimilarArtistsResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist's '{artistId}' albums does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Get artist top tracks (by default top 10)
        /// </summary>
        /// <param name="artistId">The artist (name or ID)</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="limit">Max number of results, defaulting top 10 tracks.</param>
        /// <param name="offset">Offset of results (NOT page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns>TidalTopTracksResponse object</returns>
        public async Task<TidalTopTracksResponse> AsyncGetArtistTopTracks(
            string artistId, IEnumerable<TidalFilterTypes> filterType = null, int limit = 10, int offset = 0, string countryCode = null)
        {
            var req = new TidalArtistRequest
            {
                Filters = filterType,
                Offset = offset,
                Limit = limit,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.TopTracks);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTopTracksResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving this artist's ({artistId}) top tracks does not appear to be valid. {e.Message}");
            }
        }
    }
}