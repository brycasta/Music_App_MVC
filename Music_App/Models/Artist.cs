// Written by Lee Fischer
// 2/10/25

namespace Music_App.Models
{
    public class Artist
    {
        // Private Variables
        private int artistId = -1;
        private string artistName = "n/a";
        private string description = "n/a";
        private bool isActive = true; // for extra table column


        // Gets and Sets
        public int ArtistId
        {
            get
            {
                return this.artistId;
            }
            set
            {
                this.artistId = value;
            }
        }
        public string ArtistName
        {
            get
            {
                return this.artistName;
            }
            set
            {
                this.artistName = value;
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

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        // Constructors
        public Artist (int anArtistId, string anArtistName, string aDescription)
        {
            this.ArtistId = anArtistId;
            this.ArtistName = anArtistName;
            this.Description = aDescription;
        }
        public Artist() : this(-1, "n/a", "n/a")
        {
            // Empty Constructor
        }

        // Methods
        public override string ToString()
        {
            string message = "";
            message = message + "Artist Id: " + this.ArtistId + "<br />";
            message = message + "Artist Name: " + this.ArtistName + "<br />";
            message = message + "Description: " + this.Description + "<br />";

            return message;
        }
    }
}
