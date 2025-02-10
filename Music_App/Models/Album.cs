// Written by Lee Fischer
// 2/10/25

namespace Music_App.Models
{
    public class Album
    {
        // Private Variables
        private int albumId = -1;
        private string albumName = "n/a";
        private DateTime releaseDate = DateTime.MinValue;

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

        // Constructors
        public Album(int anAlbumId, string anAlbumName, DateTime aReleaseDate)
        {
            this.albumId = anAlbumId;
            this.albumName = anAlbumName;
            this.releaseDate = aReleaseDate;
        }

        public Album() : this(-1, "n/a", DateTime.MinValue)
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
