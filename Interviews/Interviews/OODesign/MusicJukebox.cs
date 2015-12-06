using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    public class MusicJukebox
    {
        // Display Songs, Albums, Artists; Play a song; Add to queue.
        public Queue<Song> PlayList;

        public void AddSong(){}

        public List<Album> Albums;
        public List<Song> AllSongs;
        public List<Artist> Artists;

        private Dictionary<Artist, Album> ArtistToAlbums;

        private Dictionary<Album, Song> AlbumToSongs;


    }

    public class Song
    {
        public string Title;
        public Artist SongArtist;
        public Album SongAlbum;

        public void Play() { }
    }

    public class Artist
    {
        public string bio;
        public string imagePath;
    }

    public class Album
    {
        public string albumInfo;
        public string albumArtPath;
    }
}
