using Microsoft.Data.SqlClient;


namespace Music_App.Models
{
    public class DBGateway
    {
        private readonly string _connectionString;

        public DBGateway(IConfiguration configuration)  
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Artist> GetArtists()
        {
            List<Artist> artists = new List<Artist>();
             
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT artist_id, artist_name, description FROM artists", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artist anArtist = new Artist
                                {
                                    ArtistId = reader.GetInt32(0),
                                    ArtistName = reader.GetString(1),
                                    Description = reader.GetString(2)
                                };
                                artists.Add(anArtist);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    
                    throw; 
                }
            }

            return artists;
        }


        public List<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT album_id, album_title, release_date FROM albums", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Album anAlbum = new Album
                                {
                                    AlbumId = reader.GetInt32(0),
                                    AlbumName = reader.GetString(1),
                                    ReleaseDate = reader.GetDateTime(2),
                                };
                                albums.Add(anAlbum);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw;
                }
            }

            return albums;
        }
    }
}