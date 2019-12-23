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
        public string GetCurrentUserId()
        {
            return this.activeLogin.UserId;
        }

        public async Task<TidalFavouriteArtistResponse> AsyncGetMyFavouriteArtists(
            int limit = 50, string countryCode = null, int offset = 0)
        {
            var req = new TidalPaginatedRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(
                TidalUrls.Users, this.activeLogin.UserId, TidalUrls.UserFavouriteArtists);
            var result = await AsyncQueryAPI(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalFavouriteArtistResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving your favourited artists does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalFavouriteAlbumResponse> AsyncGetMyFavouriteAlbums(
            int limit = 10, string countryCode = null, int offset = 0)
        {
            var req = new TidalPaginatedRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset
            };

            var url = StringExtensions.JoinPathSegments(
                TidalUrls.Users, this.activeLogin.UserId, TidalUrls.UserFavouriteAlbums);
            var result = await AsyncQueryAPI(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalFavouriteAlbumResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving your favourited albums does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalFavouriteTracksResponse> AsyncGetMyFavouriteTracks(
            int limit = 10, string countryCode = null, int offset = 0, TidalOrderingEnum order = TidalOrderingEnum.NAME, TidalOrderingDirectionEnum orderDirection = null)
        {
            var req = new TidalOrderableRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset,
                Order = order,
                OrderDirection = orderDirection
            };

            var url = StringExtensions.JoinPathSegments(
                TidalUrls.Users, this.activeLogin.UserId, TidalUrls.UserFavouriteTracks);
            var result = await AsyncQueryAPI(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalFavouriteTracksResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving your favourited tracks does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalFavouriteVideosResponse> AsyncGetMyFavouriteVideos(
            int limit = 10, string countryCode = null, int offset = 0, TidalOrderingEnum order = TidalOrderingEnum.NAME, TidalOrderingDirectionEnum orderDirection = null)
        {
            var req = new TidalOrderableRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset,
                Order = order,
                OrderDirection = orderDirection
            };

            var url = StringExtensions.JoinPathSegments(
                TidalUrls.Users, this.activeLogin.UserId, TidalUrls.UserFavouriteVideos);
            var result = await AsyncQueryAPI(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalFavouriteVideosResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving your favourited tracks does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalMyPlaylistsResponse> AsyncGetMyPlaylists(
            int limit = 10, string countryCode = null, int offset = 0, TidalOrderingEnum order = TidalOrderingEnum.NAME, TidalOrderingDirectionEnum orderDirection = null)
        {
            var req = new TidalOrderableRequest
            {
                CountryCode = countryCode,
                Limit = limit,
                Offset = offset,
                Order = order,
                OrderDirection = orderDirection
            };

            var url = StringExtensions.JoinPathSegments(
                TidalUrls.Users, this.activeLogin.UserId, TidalUrls.MyPlaylistsAndFavourited);
            var result = await AsyncQueryAPI(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalMyPlaylistsResponse>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving your playlists does not appear to be valid. {e.Message}");
            }
        }

        public async Task<TidalUser> AsyncGetUser(string userId)
        {
            var req = new TidalRequest();

            var url = StringExtensions.JoinPathSegments(TidalUrls.Users, userId);
            var result = await AsyncQueryAPI(url, req);

            try
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TidalUser>(json);
            }
            catch (JsonException e)
            {
                throw new HttpRequestException($"The JSON returned by TIDAL for retrieving this user ({userId}) does not appear to be valid. {e.Message}");
            }
        }
    }
}