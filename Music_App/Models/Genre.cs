// Written by Lee Fischer
// 2/10/25

namespace Music_App.Models
{
    public class Genre
    {
        // Private Variables
        private int genreId = -1;
        private string genreName = "n/a";
        private double mood = 99999.99;

        // Gets and Sets
        public int GenreId
        {
            get
            {
                return this.genreId;
            }
            set
            {
                this.genreId = value;
            }
        }
        public string GenreName
        {
            get
            {
                return this.genreName;
            }
            set
            {
                this.genreName = value;
            }
        }
        public double Mood
        {
            get
            {
                return this.mood;
            }
            set
            {
                this.mood = value;
            }
        }

        // Constructors
        public Genre(int aGenreId, string aGenreName, double aMood)
        {
            this.genreId = aGenreId;
            this.genreName = aGenreName;
            this.mood = aMood;
        }
        public Genre() : this(-1, "n/a", 99999.99)
        {
            // Empty Constructor
        }

        // Methods
        public override string ToString()
        {
            string message = "";
            message = message + "Genre Id: " + this.GenreId + "<br />";
            message = message + "Genre Name: " + this.GenreName + "<br />";
            message = message + "Mood: " + this.Mood + "<br />";
            return message;
        }
    }
}
