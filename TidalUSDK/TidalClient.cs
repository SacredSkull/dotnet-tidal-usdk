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
using TidalUSDK.Extensions;
using TidalUSDK.Constants;
using TidalUSDK.Enums;
using TidalUSDK.Requests;
using TidalUSDK.Requests.Body;
using TidalUSDK.Requests.Query;
using TidalUSDK.Responses;
using TidalUSDK.Utilities;

namespace TidalUSDK
{
    public partial class TidalClient
    {
        private TidalLoginResponse activeLogin;
        private readonly SecureString secureUsername;
        private readonly SecureString securePassword;
        private readonly string token;

        /// <summary>
        ///     Paranoid? Listen to the voices in your head and pass a SecureString instead of vanilla strings.
        /// </summary>
        /// <param name="username">SecureString of your username  (read: email)</param>
        /// <param name="password">SecureString of your password</param>
        /// <param name="apiToken">
        ///     You probably don't want to play with this. This changes what comes back in terms of streaming.
        ///     Android/iOS gets a decrypted AAC stream 👌
        /// </param>
        public TidalClient(SecureString username, SecureString password, string apiToken = null)
        {
            if (apiToken == null)
            {
                apiToken = TidalNonces.TokenAndroid;
            }

            token = apiToken;

            this.secureUsername = username;
            this.securePassword = password;
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
        /// <param name="httpClientFactory">Used only to mock the http client (Flurl)</param>
        public TidalClient(string username, string password, string apiToken = null)
        {
            if (apiToken == null)
            {
                apiToken = TidalNonces.TokenAndroid;
            }

            token = apiToken;

            // SecureStrings will make the garbage collector's head spin
            var secUsername = new SecureString();
            var secPassword = new SecureString();

            foreach (var c in username)
            {
                secUsername.AppendChar(c);
            }

            foreach (var c in password)
            {
                secPassword.AppendChar(c);
            }

            this.secureUsername = secUsername;
            this.securePassword = secPassword;

            this.secureUsername.MakeReadOnly();
            this.securePassword.MakeReadOnly();
        }

        public bool IsConnected { get; private set; }

        /// <summary>
        ///     The general search API call, filterable (See <see cref="TidalQueryTypes" /> for a full listing.).
        /// </summary>
        /// <param name="query">The search query</param>
        /// <param name="queryTypes">Enumerable of types of results to return (e.g. ARTIST, TRACK, etc.).</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset of results (NOT page number) - optional, by default 0</param>
        /// <param name="countryCode">Country code - optional, default is whatever your account uses</param>
        /// <returns></returns>
        public async Task<TidalSearchResponse> SearchAsync(
            string query, TidalQueryTypes[] queryTypes, int limit = 0, int offset = 0, string countryCode = null)
        {
            var req = new TidalSearchRequest
            {
                Limit = limit,
                Query = query,
                Types = queryTypes,
                Offset = offset,
                CountryCode = countryCode
            };

            var res = await QueryAPIAsync(TidalUrls.Search.ToString(), req)
                .ConfigureAwait(false);
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
        ///
        /// </summary>
        /// <param name="coverId">GUID for art</param>
        /// <param name="width">Width of image</param>
        /// <param name="height">Height of image</param>
        /// <returns></returns>
        public Uri GetCoverUrl(Guid coverId, int width = 1280, int height = 1280)
        {
            var url = Extensions.StringExtensions.JoinPathSegments(
                TidalUrls.ResourceRoot, TidalUrls.ImagesAndCovers, coverId.ToString().Replace('-', '/'), $"{width}x{height}.jpg");

            return new Uri(url);
        }

        /// <summary>
        /// You don't need to use this.
        /// Connections are lazy-loaded, so only call this in a testing scenario.
        /// </summary>
        /// <returns></returns>
        public async Task ForceConnectAsync()
        {
            await ConnectAsync();
        }

        /// <summary>
        ///     This is automatically called if you make an API call
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private async Task ConnectAsync()
        {
            if (IsConnected)
            {
                return;
            }

            var usrPtr = Marshal.SecureStringToBSTR(this.secureUsername);
            var username = Marshal.PtrToStringBSTR(usrPtr);

            var psdPtr = Marshal.SecureStringToBSTR(this.securePassword);
            var password = Marshal.PtrToStringBSTR(psdPtr);

            Marshal.ZeroFreeBSTR(usrPtr);
            Marshal.ZeroFreeBSTR(psdPtr);

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException(
                    "You need to provide a username/email address (what you gave was null, only whitespace or empty).");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(
                    "You need to provide a password (what you gave was null, only whitespace or empty).");
            }

            var result = await LoginAsync(username, password)
                .ConfigureAwait(false);
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
        private static async Task<HttpResponseMessage> LoginAsync(string username, string password)
        {
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

        [Obsolete("Used specifically to test new/changed APIs, not for general purpose use.")]
        public async Task<HttpResponseMessage> DebugQueryAPIAsync(string relativeUri)
        {
            //BUG: remove this
            return await QueryAPIAsync(relativeUri)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     This is the basic query function, everything (except the login request) uses this function as a base.
        /// </summary>
        /// <param name="relativeUri">The relative URI to TIDAL's base API</param>
        /// <param name="request">Request object</param>
        /// <param name="countryCode">Country code</param>
        /// <param name="baseUrl">Base URL to work from</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> QueryAPIAsync(string relativeUri, TidalRequest request = null, Uri baseUrl = null)
        {
            if (!IsConnected)
            {
                await ConnectAsync();
            }

            if (request == null)
            {
                request = new TidalRequest();
            }

            // Apply defaults - if country code is set & valid, apply it on top
            request.SetDefaults(activeLogin.CountryCode);

            // Convert request to JSON and submit request
            var url = new Url(baseUrl?? TidalUrls.BaseAPI);
            return await AsyncGet(url.AppendPathSegment(relativeUri.TrimStart('/')), request)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     This is the basic query function, everything (except the login request) uses this function as a base.
        /// </summary>
        /// <param name="relativeUri">The relative URI to TIDAL's base API</param>
        /// <param name="request">Request object</param>
        /// <param name="body">String body</param>
        /// <param name="baseUrl">Base URL to work from</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> PostAPIAsync(
            string relativeUri,
            TidalEmptyRequest request = null,
            TidalEmptyBody body = null,
            Uri baseUrl = null)
        {
            if (!IsConnected)
            {
                await ConnectAsync();
            }

            if (request == null)
            {
                request = new TidalRequest();
            }

            // Apply defaults - if country code is set & valid, apply it on top
            request.SetDefaults(activeLogin.CountryCode);

            // Convert request to JSON and submit request
            var url = new Url(baseUrl ?? TidalUrls.BaseAPI);
            return await PostAsync(url.AppendPathSegment(relativeUri.TrimStart('/')), request, body)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     This is the basic query function, everything (except the login request) uses this function as a base.
        /// </summary>
        /// <param name="relativeUri">The relative URI to TIDAL's base API</param>
        /// <param name="request">Request object</param>
        /// <param name="body">String body</param>
        /// <param name="baseUrl">Base URL to work from</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> DeleteAPIAsync(
            string relativeUri,
            TidalRequest request = null,
            TidalEmptyBody body = null,
            Uri baseUrl = null)
        {
            if (!IsConnected)
            {
                await ConnectAsync();
            }

            if (request == null)
            {
                request = new TidalRequest();
            }

            // Apply defaults - if country code is set & valid, apply it on top
            request.SetDefaults(activeLogin.CountryCode);

            // Convert request to JSON and submit request
            var url = new Url(baseUrl ?? TidalUrls.BaseAPI);
            return await DeleteAsync(url.AppendPathSegment(relativeUri.TrimStart('/')), request, body)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     A very low level API call function that just performs connection checks, appends the URI to the base TIDAL URL
        ///     and constructs the request.
        /// </summary>
        /// <param name="url">The URI</param>
        /// <param name="body">POST body, as a string</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> PostAsync(Url url, object query, TidalEmptyBody body)
        {
            if (!IsConnected)
            {
                await ConnectAsync();
            }

            var json = JsonConvert.SerializeObject(query);
            var queryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            var bodyJson = JsonConvert.SerializeObject(body);
            var bodyDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(bodyJson);

            return await url
                .SetQueryParams(queryDict)
                .AttachTidalAuth(activeLogin.SessionId)
                .PostAsync(new FormUrlEncodedContent(bodyDict));
        }

        /// <summary>
        ///     Sends DELETE HTTP request to the specified URL with query/body
        /// </summary>
        /// <param name="url">The URI</param>
        /// <param name="query">Query as dictionary</param>
        /// <param name="body">POST body, as a string</param>
        /// <returns>TIDAL Response</returns>
        private async Task<HttpResponseMessage> DeleteAsync(Url url, object query, TidalEmptyBody body)
        {
            if (!IsConnected)
            {
                await ConnectAsync();
            }

            var json = JsonConvert.SerializeObject(query);
            var queryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return await url
                .SetQueryParams(queryDict)
                .AttachTidalAuth(activeLogin.SessionId)
                .SendUrlEncodedAsync(HttpMethod.Delete, body);
        }

        private async Task<HttpResponseMessage> AsyncGet(Url url, object query)
        {
            if (!IsConnected)
            {
                await ConnectAsync()
                    .ConfigureAwait(false);
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