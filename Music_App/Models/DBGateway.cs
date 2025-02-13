using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Music_App.Models
{
    public class DBGateway
    {
        private readonly string _connectionString;

        public DBGateway(IConfiguration configuration)  
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        // GetArtists and GetAlbums by Bryan Castanenda
        // Other Methods By Lee Fischer
        // 2/12/25


        public List<Artist> GetArtists()
        {
            List<Artist> artists = new List<Artist>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Artist_Id, Artist_Name, Description, Is_Active FROM Artists WHERE Is_Active = @IsActive";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter for Is_Active
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = true;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artist anArtist = new Artist
                                {
                                    ArtistId = reader.GetInt32(0),
                                    ArtistName = reader.GetString(1),
                                    Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                    IsActive = reader.GetBoolean(3)
                                };
                                artists.Add(anArtist);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Log the exception
                    // Consider using a logging framework
                    Console.WriteLine($"Database error: {ex.Message}");
                    throw;
                }
            }
            return artists;
        }


        public List<Artist> GetArtistById(int anArtistId)
        {
            List<Artist> artists = new List<Artist>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT artist_id, artist_name, description FROM artists WHERE artist_id = @artist_id", connection))
                    {
                        command.Parameters.Add("@artist_id", System.Data.SqlDbType.Int).Value = anArtistId;

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
        public List<Artist> InsertAnArtist(string anArtistName, string aDescription)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT into artists(artist_name, description)VALUES(@artist_name, @description)", connection))
                    {
                        command.Parameters.Add("@artist_name", System.Data.SqlDbType.VarChar, 55).Value = anArtistName;
                        command.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 255).Value = aDescription;

                        command.ExecuteNonQuery();
                    }

                    return GetArtists();
                }
                catch(SqlException ex)
                {
                    throw;
                }
            }
        }
        public List<Artist> UpdateAnArtist(int anArtistId, string anArtistName, string aDescription)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UPDATE artists SET artist_name = @artist_name, description = @description WHERE artist_id = @artist_id", connection))
                    {
                        command.Parameters.Add("@artist_id", System.Data.SqlDbType.Int).Value = anArtistId;
                        command.Parameters.Add("@artist_name", System.Data.SqlDbType.VarChar, 55).Value = anArtistName;
                        command.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 255).Value = aDescription;

                        command.ExecuteNonQuery();
                    }

                    return GetArtists();
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }

        public void DeactivateArtist(int artistId) // new method to set a row to deactive for delete functionality
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "UPDATE Artists SET Is_Active = 0 WHERE Artist_Id = @artistId", connection))
                    {
                        command.Parameters.AddWithValue("@artistId", artistId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }

        public List<Album> GetAlbums() //Bryan Castaneda
        {
            List<Album> albums = new List<Album>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "SELECT Album_Id, Album_Title, Release_Date, Artist_Id, Genre_Id FROM Albums",
                        connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                albums.Add(new Album(
                                    Convert.ToInt32(reader["Album_Id"]),
                                    reader["Album_Title"].ToString(),
                                    Convert.ToDateTime(reader["Release_Date"]),
                                    Convert.ToInt32(reader["Artist_Id"]),
                                    Convert.ToInt32(reader["Genre_Id"])
                                ));
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

        public List<Album> InsertAnAlbum(string anAlbumTitle, DateTime aReleaseDate, int artistId, int genreId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO albums(album_title, release_date, artist_id, genre_id) " +
                        "VALUES(@album_title, @release_date, @artist_id, @genre_id)", connection))
                    {
                        command.Parameters.Add("@album_title", System.Data.SqlDbType.VarChar, 55).Value = anAlbumTitle;
                        command.Parameters.Add("@release_date", System.Data.SqlDbType.DateTime).Value = aReleaseDate;
                        command.Parameters.Add("@artist_id", System.Data.SqlDbType.Int).Value = artistId;
                        command.Parameters.Add("@genre_id", System.Data.SqlDbType.Int).Value = genreId;
                        command.ExecuteNonQuery();
                    }
                    return GetAlbums();
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }
        public List<Song> GetSongs() //updated query to include albumid
        {
            List<Song> songs = new List<Song>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT song_id, song_title, duration, album_id FROM songs", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Song aSong = new Song
                                {
                                    SongId = reader.GetInt32(0),
                                    SongTitle = reader.GetString(1),
                                    Duration = reader.GetTimeSpan(2),
                                    AlbumId = reader.GetInt32(3)
                                };
                                songs.Add(aSong);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
            return songs;
        }
        public List<Playlist> GetPlaylists()
        {
            List<Playlist> playlists = new List<Playlist>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT playlist_id, playlist_name, description, created_date FROM playlists", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Playlist aPlaylist = new Playlist
                                {
                                    PlaylistId = reader.GetInt32(0),
                                    PlaylistName = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    CreatedDate = reader.GetDateTime(3),
                                };
                                playlists.Add(aPlaylist);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw;
                }
            }

            return playlists;
        }
        public List<Playlist> GetPlaylistById(int aPlaylistId)
        {
            List<Playlist> playlists = new List<Playlist>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT playlist_id, playlist_name, description, created_date FROM playlists WHERE playlist_id = @playlist_id", connection))
                    {
                        command.Parameters.Add("@playlist_id", System.Data.SqlDbType.Int).Value = aPlaylistId;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Playlist aPlaylist = new Playlist
                                {
                                    PlaylistId = reader.GetInt32(0),
                                    PlaylistName = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    CreatedDate = reader.GetDateTime(3),
                                };
                                playlists.Add(aPlaylist);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw;
                }
            }

            return playlists;
        }
        public List<Playlist> InsertAPlaylist(string aPlaylistName, string aDescription, DateTime aCreatedDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT into playlists(playlist_name, description, created_date)VALUES(@playlist_name, @description, @created_date)", connection))
                    {
                        command.Parameters.Add("@playlist_name", System.Data.SqlDbType.VarChar, 55).Value = aPlaylistName;
                        command.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 255).Value = aDescription;
                        command.Parameters.Add("@created_date", System.Data.SqlDbType.DateTime).Value = aCreatedDate;

                        command.ExecuteNonQuery();
                    }

                    return GetPlaylists();
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }
        public List<Playlist> UpdateAPlaylist(int aPlaylistId, string aPlaylistName, string aDescription, DateTime aCreatedDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UPDATE playlists SET playlist_name = @playlist_name, description = @description, created_date = @created_date WHERE playlist_id = @playlist_id", connection))
                    {
                        command.Parameters.Add("@playlist_id", System.Data.SqlDbType.Int).Value = aPlaylistId;
                        command.Parameters.Add("@playlist_name", System.Data.SqlDbType.VarChar, 55).Value = aPlaylistName;
                        command.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 255).Value = aDescription;
                        command.Parameters.Add("@created_date", System.Data.SqlDbType.DateTime).Value = aCreatedDate;

                        command.ExecuteNonQuery();
                    }

                    return GetPlaylists();
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }
        public List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT genre_id, genre_name, mood FROM genres", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Genre aGenre = new Genre
                                {
                                    GenreId = reader.GetInt32(0),
                                    GenreName = reader.GetString(1),
                                    Mood = reader.GetDecimal(2),
                                };
                                genres.Add(aGenre);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw;
                }
            }

            return genres;
        }
    }
}