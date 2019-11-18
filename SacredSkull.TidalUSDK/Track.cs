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
        ///     Get specific track information
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>TidalTrackResponse / TidalTrack</returns>
        /// <exception cref="HttpRequestException">If JSON is invalid</exception>
        public async Task<TidalTrack> AsyncGetTrack(string trackId, string countryCode = null)
        {
            var req = new TidalRequest
            {
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrack>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving track info ID: {trackId} does not appear to be valid. {e.Message}");
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
        public async Task<TidalTrackStreamingURLResponse> AsyncGetTrackStreamingURL(string trackId, TidalStreamingQualityEnum streamQuality, string countryCode = null)
        {
            var req = new TidalStreamingRequest
            {
                StreamQuality = streamQuality,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId, TidalUrls.TrackStreamingURL);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrackStreamingURLResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving track info ID: {trackId} does not appear to be valid. {e.Message}");
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
        public async Task<TidalTrackStreamingURLResponse> AsyncGetTrackOfflineStreamingURL(string trackId, TidalStreamingQualityEnum streamQuality, string countryCode = null)
        {
            var req = new TidalStreamingRequest
            {
                StreamQuality = streamQuality,
                CountryCode = countryCode
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId, TidalUrls.OfflineStreamingURL);
            var result = await AsyncQueryAPI(url, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrackStreamingURLResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving track info ID: {trackId} does not appear to be valid. {e.Message}");
            }
        }
    }
}