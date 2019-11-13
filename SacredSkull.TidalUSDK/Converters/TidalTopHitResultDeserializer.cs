using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SacredSkull.TidalUSDK.Entities;
using SacredSkull.TidalUSDK.Enums;
using SacredSkull.TidalUSDK.Extensions;

namespace SacredSkull.TidalUSDK.Converters
{
    /// <summary>
    ///     TopHitResults are complex since they return a type and value property - so, we have to implement more complex logic
    ///     to handle it
    ///     (the actual class is more complex too, such as throwing exceptions if you try to access the incorrect property.
    /// </summary>
    internal class TidalTopHitResultDeserializer : JsonConverter<TidalTopHit>
    {
        public override void WriteJson(JsonWriter writer, TidalTopHit value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override TidalTopHit ReadJson(JsonReader reader, Type objectType, TidalTopHit existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            JObject token = JObject.Load(reader);

            // We need a value and type to work with, if these are missing,
            if (!token.ContainsKey("value") || !token.ContainsKey("type"))
            {
                throw new JsonException($"Unrecognised JSON for TopHitResult! Please report this exception. {token}");
            }

            var value = token["value"];
            var type = token["type"];

            // Attempt to convert the type property to our TidalResultType enum, if case fails, throw an exception.
            if (!Enum.TryParse(type.ToString(), true, out TidalResultTypes tidalResultType))
            {
                throw new ArgumentOutOfRangeException(
                    $"Attempted to convert an unknown top hit type to a TopHit result object - (returned {type}). Please report this exception. {token}");
            }

            var topHit = new TidalTopHit
            {
                Type = tidalResultType
            };

            switch (tidalResultType)
            {
                case TidalResultTypes.ALBUMS:
                    topHit.TopAlbum = value.IsNullOrEmpty() ? null : value.ToObject<TidalAlbum>();
                    break;
                case TidalResultTypes.ARTISTS:
                    topHit.TopArtist = value.IsNullOrEmpty() ? null : value.ToObject<TidalArtist>();
                    break;
                case TidalResultTypes.PLAYLISTS:
                    topHit.TopPlaylist = value.IsNullOrEmpty() ? null : value.ToObject<TidalPlaylist>();
                    break;
                case TidalResultTypes.TRACKS:
                    topHit.TopTrack = value.IsNullOrEmpty() ? null : value.ToObject<TidalTrack>();
                    break;
                case TidalResultTypes.VIDEOS:
                    topHit.TopVideo = value.IsNullOrEmpty() ? null : value.ToObject<TidalVideo>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"USDK has an enum but not been set up to process TidalTopHitResult type '{tidalResultType.ToString()}' in the TidalTopHitResultDeserializer, please report this exception message. {token}");
            }

            return topHit;
        }
    }
}