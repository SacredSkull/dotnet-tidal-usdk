using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TidalUSDK.Constants;
using TidalUSDK.Entities;
using TidalUSDK.Enums;
using TidalUSDK.Extensions;
using TidalUSDK.Requests;
using TidalUSDK.Requests.Body;
using TidalUSDK.Responses;

namespace TidalUSDK
{
    public partial class TidalClient
    {
/// <summary>
        ///     Get specific track information
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>TidalTrackResponse / TidalTrack</returns>
        /// <exception cref="HttpRequestException">If JSON is invalid</exception>
        public async Task<TidalTrack> GetTrackAsync(string trackId, string countryCode = null)
        {
            var req = new TidalRequest
            {
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId);
            var result = await QueryAPIAsync(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrack>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException(
                    $"The JSON returned by TIDAL for retrieving track info ID: {trackId} does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Gets streaming URL for given track
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param mame="streamQuality">Stream quality</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>The streaming URL for the given track</returns>
        /// <exception cref="HttpRequestException">Invalid JSON returned</exception>
        public async Task<TidalTrackStreamingURLResponse> GetTrackStreamingURLAsync(
            string trackId,
            TidalStreamingQualityEnum streamQuality,
            string countryCode = null)
        {
            var req = new TidalStreamingRequest
            {
                StreamQuality = streamQuality,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId, TidalUrls.TrackStreamingURL);
            var result = await QueryAPIAsync(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrackStreamingURLResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException(
                    $"The JSON returned by TIDAL for retrieving track info ID: {trackId} does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Gets offline streaming URL for given track
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param name="streamQuality">Stream quality</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>The streaming URL for the given track</returns>
        /// <exception cref="HttpRequestException">Invalid JSON returned</exception>
        public async Task<TidalTrackStreamingURLResponse> GetTrackOfflineStreamingURLAsync(
            string trackId,
            TidalStreamingQualityEnum streamQuality,
            string countryCode = null)
        {
            var req = new TidalStreamingRequest
            {
                StreamQuality = streamQuality,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId, TidalUrls.OfflineStreamingURL);
            var result = await QueryAPIAsync(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrackStreamingURLResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException(
                    $"The JSON returned by TIDAL for retrieving track info ID: {trackId} does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Adds a track to your library (favourites list)
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param mame="streamQuality">Stream quality</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>The streaming URL for the given track</returns>
        /// <exception cref="HttpRequestException">Invalid JSON returned</exception>
        /// <remarks>
        ///     The TIDAL app has its own library cache, so it might seem like you haven't changed anything.
        ///     Open a web player (i.e. listen.tidal.com) and you should see the new library change.
        /// </remarks>
        public async Task<HttpResponseMessage> AddTrackToMyLibraryAsync(string trackId, string countryCode = null)
        {
            var req = new TidalRequest
            {
                CountryCode = countryCode
            };

            var body = new TidalTrackFavoriteBody
            {
                TrackId = trackId
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Users, this.activeLogin.UserId, TidalUrls.UserFavouriteTracks);
            return await PostAPIAsync(url, req, body);
        }

        /// <summary>
        ///     Removes a track from your library (favourites list)
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param mame="streamQuality">Stream quality</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>The streaming URL for the given track</returns>
        /// <exception cref="HttpRequestException">Invalid JSON returned</exception>
        /// <remarks>
        ///     The TIDAL app has its own library cache, so it might seem like you haven't changed anything.
        ///     Open a web player (i.e. listen.tidal.com) and you should see the new library change.
        /// </remarks>
        public async Task<HttpResponseMessage> RemoveTrackFromMyLibraryAsync(string trackId, string countryCode = null)
        {
            var req = new TidalRequest
            {
                CountryCode = countryCode
            };

            var body = new TidalTrackFavoriteBody
            {
                TrackId = trackId
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Users, this.activeLogin.UserId,
                TidalUrls.UserFavouriteTracks, trackId);
            return await DeleteAPIAsync(url, req, body);
        }
    }
}