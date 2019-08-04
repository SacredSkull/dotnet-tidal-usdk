using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TidalUSDK.Constants;
using TidalUSDK.Enums;
using TidalUSDK.Requests;
using Flurl;
using Flurl.Http;
using TidalUSDK.Extensions;
using TidalUSDK.Responses;

namespace TidalUSDK {
    public class TidalConnection {
        public bool IsConnected { get; private set; }

        private SecureString username;
        private SecureString password;
        private string token;
        private HttpClient httpClient = new HttpClient();
        private TidalLoginResponse activeLogin;

        /// <summary>
        /// Paranoid? Listen to the voices in your head and pass a SecureString instead of vanilla strings.
        /// </summary>
        /// <param name="username">SecureString of your username  (read: email)</param>
        /// <param name="password">SecureString of your password</param>
        /// <param name="apiToken">You probably don't want to play with this. This changes what comes back in terms of streaming. Android/iOS gets a decrypted AAC stream 👌</param>
        public TidalConnection(SecureString username, SecureString password, string apiToken = null) {
            if (apiToken == null)
                apiToken = TidalNonces.TokenAndroid;
            this.token = apiToken;

            this.username = username;
            this.password = password;
        }
        
        /// <summary>
        /// Laid back? Ignore any shame and pass in plain ol' strings - don't worry, I'll convert them into SecureStrings for you, lazy prick.
        /// </summary>
        /// <param name="username">(NotSo)SecureString of your username  (read: email)</param>
        /// <param name="password">(NotSo)SecureString of your password</param>
        /// <param name="apiToken">You probably don't want to play with this. This changes what comes back in terms of streaming. Android/iOS gets a decrypted AAC stream 👌</param>
        public TidalConnection(string username, string password, string apiToken = null) {
            if (apiToken == null)
                apiToken = TidalNonces.TokenAndroid;
            this.token = apiToken;
            
            // SecureStrings will make the garbage collector's head spin
            var secureUsername = new SecureString();
            var securePassword = new SecureString();

            foreach (var c in username) {
                secureUsername.AppendChar(c);
            }

            foreach (var c in password) {
                securePassword.AppendChar(c);
            }

            this.username = secureUsername;
            this.password = securePassword;
            
            this.username.MakeReadOnly();
            this.password.MakeReadOnly();
        }

        /// <summary>
        /// The general search API call, filterable (See <see cref="TidalQueryTypes"/> for a full listing.).
        /// </summary>
        /// <param name="query">The search query</param>
        /// <param name="queryTypes">Enumerable of types of results to return (e.g. ARTIST, TRACK, etc.).</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalSearchResponse> AsyncSearch(string query, TidalQueryTypes[] queryTypes, int limit = 0, int offset = 0, string countryCode = null) {
            var req = new TidalSearchRequest {
                Limit = limit,
                Query = query,
                Types = queryTypes,
                Offset = offset
            };

            var res = await AsyncQueryAPI(TidalUrls.Search.ToString(), req, countryCode);
            try {
                var json = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalSearchResponse>(json);
            } catch (JsonException e) {
                throw new HttpRequestException($"The JSON returned by TIDAL for searching does not appear to be valid. {e.Message}");
            }
        }

        /// <summary>
        /// Get artist information, not including bio.
        /// See also:
        /// <seealso cref="AsyncGetArtistBio"/>
        /// <seealso cref="AsyncGetArtistTopTracks"/>
        /// </summary>
        /// <param name="artist">The artist's TIDAL ID</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtistResponse> AsyncGetArtist(string artist, IEnumerable<TidalFilterTypes> filterType = null, int limit = 0, int offset = 0, string countryCode = null) {
            var req = new TidalArtistRequest {
                Filters = filterType,
                Offset = offset,
                Limit = limit
            };

            var result = await AsyncQueryAPI(TidalUrls.Artists + $"/{artist}", req, countryCode);
            try {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistResponse>(json);
            } catch (JsonException e) {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{artist}' does not appear to be valid. {e.Message}");
            }
        }
        
        /// <summary>
        /// Get artist bio
        /// </summary>
        /// <param name="artist">The artist (name or ID)</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalArtistBioResponse> AsyncGetArtistBio(string artist, string countryCode = null) {
            var result = await AsyncQueryAPI(TidalUrls.Artists + $"/{artist}/" + TidalUrls.Bio, new TidalArtistRequest(), countryCode);
            try {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalArtistBioResponse>(json);
            } catch (JsonException e) {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving artist '{artist}' does not appear to be valid. {e.Message}");
            }
        }
        
        /// <summary>
        /// Get artist top tracks (by default top 10)
        /// </summary>
        /// <param name="artist">The artist (name or ID)</param>
        /// <param name="filterType">Query type(s) you wish to filterTypes by</param>
        /// <param name="limit">Max number of results, defaulting top 10 tracks.</param>
        /// <param name="offset">Offset of results (think page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalTopTracksResponse> AsyncGetArtistTopTracks(string artist, IEnumerable<TidalFilterTypes> filterType, int limit = 10, int offset = 0, string countryCode = null) {
            var req = new TidalArtistRequest {
                Filters = filterType,
                Offset = offset,
                Limit = limit
            };

            var result = await AsyncQueryAPI(TidalUrls.Artists + $"/{artist}/" + TidalUrls.TopTracks , req, countryCode);
            try {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalTopTracksResponse>(json);
            } catch (JsonException e) {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving this artist's ({artist}) top tracks does not appear to be valid. {e.Message}");
            }
        }
        
        /// <summary>
        /// This is automatically called if you make an API call
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private async Task Connect() {
            if (this.IsConnected)
                return;
            
            if (string.IsNullOrWhiteSpace(this.username.ToString())) {
                throw new ArgumentException("You need to provide a username/email address (what you gave was null, whitespace or empty).");
            }
            
            if (string.IsNullOrWhiteSpace(this.password.ToString())) {
                throw new ArgumentException("You need to provide a password (what you gave was null, whitespace or empty).");
            }

            var result = await this.AsyncLogin();
            result.EnsureSuccessStatusCode();

            try {
                string responseBody = await result.Content.ReadAsStringAsync();
                this.activeLogin = JsonConvert.DeserializeObject<TidalLoginResponse>(responseBody);
                this.IsConnected = true;
            } catch (JsonException e) {
                this.IsConnected = false;
                throw new AuthenticationException($"Attempted to login to TIDAL, but the JSON that was returned is not valid. Further information: {e.Message}");
            }
        }

        /// <summary>
        /// Performs the login request, most importantly, retrieving the Session ID
        /// </summary>
        /// <returns></returns>
        private async Task<HttpResponseMessage> AsyncLogin() {
            IntPtr usrPtr = Marshal.SecureStringToBSTR(this.username);
            string username = Marshal.PtrToStringBSTR(usrPtr);
            
            IntPtr psdPtr = Marshal.SecureStringToBSTR(this.password);
            string password = Marshal.PtrToStringBSTR(psdPtr);
            
            Marshal.ZeroFreeBSTR(usrPtr);
            Marshal.ZeroFreeBSTR(psdPtr);
            
            var url = new Url(TidalUrls.BaseAPI);

            return await url
                .AppendPathSegment(TidalUrls.Login.ToString())
                .WithHeader(TidalHeaders.ClientTypeToken, TidalNonces.TokenAndroid)
                .PostUrlEncodedAsync(new Dictionary<string, string> {
                    { TidalHeaders.Username, username },
                    { TidalHeaders.Password, password },
                    { TidalHeaders.ClientUniqueKey, TidalNonces.ClientKey }
                });
        }

        /// <summary>
        /// This is the basic query function, everything (except the login request) uses this function as a base.
        /// </summary>
        /// <param name="relativeUri">The relative URI to TIDAL's base API</param>
        /// <param name="request">Request object</param>
        /// <param name="countryCode">Country code</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> AsyncQueryAPI(string relativeUri, TidalBaseRequest request, string countryCode) {
            if (!this.IsConnected) {
                await this.Connect();
            }
            
            // Apply defaults - if country code is set & valid, apply it on top
            request.SetDefaults(string.IsNullOrWhiteSpace(countryCode) ? this.activeLogin.CountryCode : countryCode);

            // Convert request to JSON and submit request
            var url = new Url(TidalUrls.BaseAPI);
            return await this.AsyncGet(url.AppendPathSegment(relativeUri.TrimStart('/')), request);
        }

        /// <summary>
        /// A very low level API call function that just performs connection checks, appends the URI to the base TIDAL URL
        /// and constructs the request.
        /// </summary>
        /// <param name="url">The URI</param>
        /// <param name="body">POST body, as a string</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> AsyncPost(Url url, string body) {
            if (!this.IsConnected) {
                await this.Connect();
            }
            
            return await url
                .PostAsync(new StringContent(body, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json));
        }
        
        private async Task<HttpResponseMessage> AsyncGet(Url url, object query) {
            if (!this.IsConnected) {
                await this.Connect();
            }
            
            var json = JsonConvert.SerializeObject(query);
            var queryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            return await url
                .SetQueryParams(queryDict)
                .AttachTidalAuth(this.activeLogin.SessionId)
                .GetAsync();
        }
    }
}