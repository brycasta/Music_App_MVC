// Written by Lee Fischer
// 2/10/25

namespace Music_App.Models
{
    public class Album
    {
        // Private Variables
        private int albumId = -1;
        private int artistId = -1; //added another variable for fk
        private string albumName = "n/a";
        private DateTime releaseDate = DateTime.MinValue;
        private int genreId = -1;

        // Gets and Sets



        public int AlbumId
        {
            get
            {
                return this.albumId;
            }
            set
            {
                this.albumId = value;
            }
        }
        public string AlbumName
        {
            get
            {
                return this.albumName;
            }
            set
            {
                this.albumName = value;
            }
        }
        public DateTime ReleaseDate
        {
            get
            {
                return this.releaseDate;
            }
            set
            {
                this.releaseDate = value;
            }
        }


        public int ArtistId // added this for filtering Bryan Castaneda
        {
            get { return this.artistId; }
            set { this.artistId = value; }
        }

        public int GenreId
        {
            get { return this.genreId; }
            set { this.genreId = value; }
        }


        // Constructors
        //updated constructor Bryan Castaneda
        public Album(int anAlbumId, string anAlbumName, DateTime aReleaseDate, int anArtistId, int aGenreId)
        {
            this.albumId = anAlbumId;
            this.albumName = anAlbumName;
            this.releaseDate = aReleaseDate;
            this.artistId = anArtistId;
            this.genreId = aGenreId;
        }

        public Album() : this(-1, "n/a", DateTime.MinValue, -1, -1)
        {
            // Empty Constructor

        }

            // Methods
        public override string ToString()
        {
            string message = "";
            message = message + "Album Id: " + this.AlbumId + "<br />";
            message = message + "Album Name: " + this.AlbumName + "<br />";
            message = message + "Release Date: " + this.ReleaseDate + "<br />";
            return message;
        }
    }
}
