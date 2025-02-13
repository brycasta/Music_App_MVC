// Written by Lee Fischer
// 2/10/25

namespace Music_App.Models
{
    public class Song
    {
        // Private Variables
        private int songId = -1;
        private string songTitle = "n/a";
        private TimeSpan duration = TimeSpan.MinValue;

        // Gets and Sets
        public int SongId
        {
            get
            {
                return this.songId;
            }
            set
            {
                this.songId = value;
            }
        }
        public string SongTitle
        {
            get
            {
                return this.songTitle;
            }
            set
            {
                this.songTitle = value;
            }
        }
        public TimeSpan Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        // Constructors
        public Song(int aSongId, string aSongTitle, TimeSpan aDuration)
        {
            this.songId = aSongId;
            this.songTitle = aSongTitle;
            this.duration = aDuration;
        }
        public Song() : this(-1, "n/a", TimeSpan.MinValue)
        {
            // Empty Constructor
        }

        // Methods
        public override string ToString()
        {
            string message = "";
            message = message + "Song Id: " + this.SongId +"<br />";
            message = message + "Song Title: " + this.SongTitle + "<br />";
            message = message + "Duration: " + this.Duration + "<br />";
            return message;
        }
    }
}
