// Written by Lee Fischer
// 2/10/25

namespace Music_App.Models
{
    public class Playlist
    {
        // Private Variables
        private int playlistId = -1;
        private string playlistName = "n/a";
        private string description = "n/a";
        private DateTime createdDate = DateTime.MinValue;

        // Gets and Sets
        public int PlaylistId
        {
            get
            {
                return this.playlistId;
            }
            set
            {
                this.playlistId = value;
            }
        }
        public string PlaylistName
        {
            get
            {
                return this.playlistName;
            }
            set
            {
                this.playlistName = value;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set
            {
                this.createdDate = value;
            }
        }

        // Constructors
        public Playlist(int aPlaylistId, string aPlaylistName, string aDescription, DateTime aCreatedDate)
        {
            this.playlistId = aPlaylistId;
            this.playlistName = aPlaylistName;
            this.description = aDescription;
            this.createdDate = aCreatedDate;
        }
        public Playlist() : this(-1, "n/a", "n/a", DateTime.MinValue)
        {
            // Empty Constructor
        }

        // Methods
        public override string ToString()
        {
            string message = "";
            message = message + "Playlist Id: " + this.PlaylistId + "<br />";
            message = message + "Playlist Name: " + this.PlaylistName + "<br />";
            message = message + "Description: " + this.Description + "<br />";
            message = message + "Created Date: " + this.CreatedDate + "<br />";
            return message;
        }
    }
}
