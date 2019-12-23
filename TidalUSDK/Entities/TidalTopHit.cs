using System;
using Newtonsoft.Json;
using TidalUSDK.Converters;
using TidalUSDK.Enums;

namespace TidalUSDK.Entities
{
    [JsonConverter(typeof(TidalTopHitResultDeserializer))]
    public class TidalTopHit
    {
        private TidalAlbum _topAlbum;

        private TidalArtist _topArtist;

        private TidalPlaylist _topPlaylist;

        private TidalTrack _topTrack;

        private TidalVideo _topVideo;

        /// <summary>
        ///     This property will tell you the type of TopHit that was returned.
        ///     You MUST check this property to
        /// </summary>
        public TidalResultTypes Type { get; set; }

        public TidalArtist TopArtist
        {
            get
            {
                if (Type != TidalResultTypes.ARTISTS)
                {
                    AccessInvalidTopHit("Artists");
                }

                return _topArtist;
            }

            set => _topArtist = value;
        }

        public TidalAlbum TopAlbum
        {
            get
            {
                if (Type != TidalResultTypes.ALBUMS)
                {
                    AccessInvalidTopHit("Albums");
                }

                return _topAlbum;
            }

            set => _topAlbum = value;
        }

        public TidalPlaylist TopPlaylist
        {
            get
            {
                if (Type != TidalResultTypes.PLAYLISTS)
                {
                    AccessInvalidTopHit("Playlists");
                }

                return _topPlaylist;
            }

            set => _topPlaylist = value;
        }

        public TidalTrack TopTrack
        {
            get
            {
                if (Type != TidalResultTypes.TRACKS)
                {
                    AccessInvalidTopHit("Tracks");
                }

                return _topTrack;
            }
            set => _topTrack = value;
        }

        public TidalVideo TopVideo
        {
            get
            {
                if (Type != TidalResultTypes.VIDEOS)
                {
                    AccessInvalidTopHit("Videos");
                }

                return _topVideo;
            }
            set => _topVideo = value;
        }

        private static void AccessInvalidTopHit(string property)
        {
            throw new InvalidOperationException(
                $"You tried to read property {property}, " +
                "but this wasn't the type of top hit returned. " +
                "Use the Type property to make sure you use the correct parameter, " +
                "or access ");
        }
    }
}