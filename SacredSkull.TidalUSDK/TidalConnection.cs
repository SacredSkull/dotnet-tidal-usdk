using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using SacredSkull.TidalUSDK.Constants;
using SacredSkull.TidalUSDK.Enums;
using SacredSkull.TidalUSDK.Extensions;
using SacredSkull.TidalUSDK.Requests;
using SacredSkull.TidalUSDK.Responses;
using StringExtensions = SacredSkull.TidalUSDK.Extensions.StringExtensions;

namespace SacredSkull.TidalUSDK
{
    public class TidalConnection
    {
        private TidalLoginResponse activeLogin;
        private readonly SecureString password;
        private string token;

        private readonly SecureString username;

        /// <summary>
        ///     Paranoid? Listen to the voices in your head and pass a SecureString instead of vanilla strings.
        /// </summary>
        /// <param name="username">SecureString of your username  (read: email)</param>
        /// <param name="password">SecureString of your password</param>
        /// <param name="apiToken">
        ///     You probably don't want to play with this. This changes what comes back in terms of streaming.
        ///     Android/iOS gets a decrypted AAC stream 👌
        /// </param>
        public TidalConnection(SecureString username, SecureString password, string apiToken = null)
        {
            if (apiToken == null)
                apiToken = TidalNonces.TokenAndroid;
            token = apiToken;

            this.username = username;
            this.password = password;
        }

        /// <summary>
        ///     Too lazy to provide your own SecureStrings? No problem, I'll do it for you.
        /// </summary>
        /// <param name="username">(NotSo)SecureString of your username  (read: email)</param>
        /// <param name="password">(NotSo)SecureString of your password</param>
        /// <param name="apiToken">
        ///     You probably don't want to play with this. This changes what comes back in terms of streaming.
        ///     Android/iOS gets a decrypted AAC stream 👌
        /// </param>
        public TidalConnection(string username, string password, string apiToken = null)
        {
            if (apiToken == null)
                apiToken = TidalNonces.TokenAndroid;
            token = apiToken;

            // SecureStrings will make the garbage collector's head spin
            var secureUsername = new SecureString();
            var securePassword = new SecureString();

            foreach (var c in username)
            {
                secureUsername.AppendChar(c);
            }

            foreach (var c in password)
            {
                securePassword.AppendChar(c);
            }

            this.username = secureUsername;
            this.password = securePassword;

            this.username.MakeReadOnly();
            this.password.MakeReadOnly();
        }

        public bool IsConnected { get; private set; }

        /// <summary>
        ///     The general search API call, filterable (See <see cref="TidalQueryTypes" /> for a full listing.).
        /// </summary>
        /// <param name="query">The search query</param>
        /// <param name="queryTypes">Enumerable of types of results to return (e.g. ARTIST, TRACK, etc.).</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalSearchResponse> AsyncSearch(
            string query, TidalQueryTypes[] queryTypes, int limit = 0, int offset = 0, string countryCode = null)
        {
            var req = new TidalSearchRequest
            {
                Limit = limit,
                Query = query,
                Types = queryTypes,
                Offset = offset
            };

            var res = await AsyncQueryAPI(TidalUrls.Search.ToString(), countryCode, req);
            try
            {
                var json = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalSearchResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for searching does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Get artist information, not including bio.
        ///     See also:
        ///     <seealso cref="AsyncGetArtistBio" />
        ///     <seealso cref="AsyncGetArtistTopTracks" />
        /// </summary>
        /// <param name="artistId">The artist's TIDAL ID</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtistResponse> AsyncGetArtist(
            string artistId, IEnumerable<TidalFilterTypes> filterType = null, int limit = 0, int offset = 0, string countryCode = null)
        {
            var req = new TidalArtistRequest
            {
                Filters = filterType,
                Offset = offset,
                Limit = limit
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId);
            var result = await AsyncQueryAPI(url, countryCode, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistResponse>(json);
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
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtistVideoesResponse> AsyncGetArtistVideos(string artistId, IEnumerable<TidalFilterTypes> filterType = null, int limit = 0, int offset = 0, string countryCode = null)
        {
            var req = new TidalFilterableRequest
            {
                Filters = filterType,
                Offset = offset,
                Limit = limit
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.Videos);
            var result = await AsyncQueryAPI(url, countryCode, req);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistVideoesResponse>(json);
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
        public async Task<TidalArtistBioResponse> AsyncGetArtistBio(string artistId, string countryCode = null)
        {
            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.Bio);
            var result = await AsyncQueryAPI(url, countryCode, new TidalArtistRequest());
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistBioResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{artistId}' does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        ///     Get specific track information
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>TidalTrackResponse / TidalTrack</returns>
        /// <exception cref="HttpRequestException">If JSON is invalid</exception>
        public async Task<TidalTrackResponse> AsyncGetTrack(string trackId, string countryCode = null)
        {
            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId);
            var result = await AsyncQueryAPI(url, countryCode);
            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTrackResponse>(json);
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
                StreamQuality = streamQuality
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId, TidalUrls.TrackStreamingURL);
            var result = await AsyncQueryAPI(url, countryCode, req);
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
                StreamQuality = streamQuality
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Tracks, trackId, TidalUrls.OfflineStreamingURL);
            var result = await AsyncQueryAPI(url, countryCode, req);
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
        ///     Get artist top tracks (by default top 10)
        /// </summary>
        /// <param name="artistId">The artist (name or ID)</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="limit">Max number of results, defaulting top 10 tracks.</param>
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns>TidalTopTracksResponse object</returns>
        public async Task<TidalTopTracksResponse> AsyncGetArtistTopTracks(
            string artistId, IEnumerable<TidalFilterTypes> filterType, int limit = 10, int offset = 0, string countryCode = null)
        {
            var req = new TidalArtistRequest
            {
                Filters = filterType,
                Offset = offset,
                Limit = limit
            };

            var url = StringExtensions.JoinPathSegments(TidalUrls.Artists, artistId, TidalUrls.TopTracks);
            var result = await AsyncQueryAPI(url, countryCode, req);
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

        /// <summary>
        ///     This is automatically called if you make an API call
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private async Task Connect()
        {
            if (IsConnected)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(username.ToString()))
            {
                throw new ArgumentException(
                    "You need to provide a username/email address (what you gave was null, whitespace or empty).");
            }

            if (string.IsNullOrWhiteSpace(password.ToString()))
            {
                throw new ArgumentException(
                    "You need to provide a password (what you gave was null, whitespace or empty).");
            }

            var result = await AsyncLogin();
            result.EnsureSuccessStatusCode();

            try
            {
                string responseBody = await result.Content.ReadAsStringAsync();
                activeLogin = JsonConvert.DeserializeObject<TidalLoginResponse>(responseBody);
                IsConnected = true;
            }
            catch (JsonException e)
            {
                IsConnected = false;
                throw new AuthenticationException(
                    $"Attempted to login to TIDAL, but the JSON that was returned is not valid. Further information: {e.Message}");
            }
        }

        /// <summary>
        ///     Performs the login request, most importantly, retrieving the Session ID
        /// </summary>
        private async Task<HttpResponseMessage> AsyncLogin()
        {
            var usrPtr = Marshal.SecureStringToBSTR(this.username);
            var username = Marshal.PtrToStringBSTR(usrPtr);

            var psdPtr = Marshal.SecureStringToBSTR(this.password);
            var password = Marshal.PtrToStringBSTR(psdPtr);

            Marshal.ZeroFreeBSTR(usrPtr);
            Marshal.ZeroFreeBSTR(psdPtr);

            var url = new Url(TidalUrls.BaseAPI);
            return await url
                .AppendPathSegment(TidalUrls.Login.ToString())
                .WithHeader(TidalHeaders.ClientTypeToken, TidalNonces.TokenAndroid)
                .PostUrlEncodedAsync(new Dictionary<string, string>
                {
                    {TidalHeaders.Username, username},
                    {TidalHeaders.Password, password},
                    {TidalHeaders.ClientUniqueKey, TidalNonces.ClientKey}
                });
        }

        /// <summary>
        ///     This is the basic query function, everything (except the login request) uses this function as a base.
        /// </summary>
        /// <param name="relativeUri">The relative URI to TIDAL's base API</param>
        /// <param name="request">Request object</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> AsyncQueryAPI(string relativeUri, string countryCode, TidalRequest request = null)
        {
            if (!IsConnected)
            {
                await Connect();
            }

            if (request == null)
            {
                request = new TidalRequest();
            }

            // Apply defaults - if country code is set & valid, apply it on top
            request.SetDefaults(string.IsNullOrWhiteSpace(countryCode)
                ? activeLogin.CountryCode
                : countryCode);

            // Convert request to JSON and submit request
            var url = new Url(TidalUrls.BaseAPI);
            return await AsyncGet(url.AppendPathSegment(relativeUri.TrimStart('/')), request);
        }

        /// <summary>
        ///     A very low level API call function that just performs connection checks, appends the URI to the base TIDAL URL
        ///     and constructs the request.
        /// </summary>
        /// <param name="url">The URI</param>
        /// <param name="body">POST body, as a string</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> AsyncPost(Url url, string body)
        {
            if (!IsConnected)
            {
                await Connect();
            }

            return await url
                .PostAsync(new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json));
        }

        private async Task<HttpResponseMessage> AsyncGet(Url url, object query)
        {
            if (!IsConnected)
            {
                await Connect();
            }

            var json = JsonConvert.SerializeObject(query);
            var queryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return await url
                .SetQueryParams(queryDict)
                .AttachTidalAuth(activeLogin.SessionId)
                .GetAsync();
        }
    }
}