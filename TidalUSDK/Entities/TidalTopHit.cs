using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TidalUSDK.Deserializers;
using TidalUSDK.Enums;

namespace TidalUSDK.Entities {
    [JsonConverter(typeof(TidalTopHitResultDeserializer))]
    public class TidalTopHit
    {
        /// <summary>
        /// This property will tell you the type of TopHit that was returned.
        /// You MUST check this property to
        /// </summary>
        public TidalResultTypes Type { get; set; }

        private TidalArtist _topArtist;
        public TidalArtist TopArtist
        {
            get
            {
                if (this.Type != TidalResultTypes.ARTISTS)
                {
                    AccessInvalidTopHit("Artists");
                }

                return _topArtist;
            }

            set => _topArtist = value;
        }

        private TidalAlbum _topAlbum;
        public TidalAlbum TopAlbum
        {
            get
            {
                if (this.Type != TidalResultTypes.ALBUMS)
                {
                    AccessInvalidTopHit("Albums");
                }

                return _topAlbum;
            }

            set => _topAlbum = value;
        }

        private TidalPlaylist _topPlaylist;
        public TidalPlaylist TopPlaylist
        {
            get
            {
                if (this.Type != TidalResultTypes.PLAYLISTS)
                {
                    AccessInvalidTopHit("Playlists");
                }

                return _topPlaylist;
            }

            set => _topPlaylist = value;
        }

        private TidalTrack _topTrack;
        public TidalTrack TopTrack
        {
            get
            {
                if (this.Type != TidalResultTypes.TRACKS)
                {
                    AccessInvalidTopHit("Tracks");
                }

                return _topTrack;
            }
            set => _topTrack = value;
        }

        private TidalVideo _topVideo;
        public TidalVideo TopVideo
        {
            get
            {
                if (this.Type != TidalResultTypes.VIDEOS)
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
                $"but this wasn't the type of top hit returned. " +
                $"Use the Type property to make sure you use the correct parameter, " +
                $"or access ");
        }
    }
}