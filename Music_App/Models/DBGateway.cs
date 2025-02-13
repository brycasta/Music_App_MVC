using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
        public List<Album> InsertAnAlbum(string anAlbumTitle, DateTime aReleaseDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT into albums(album_title, release_date)VALUES(@album_title, @release_date)", connection))
                    {
                        command.Parameters.Add("@album_title", System.Data.SqlDbType.VarChar, 55).Value = anAlbumTitle;
                        command.Parameters.Add("@release_date", System.Data.SqlDbType.DateTime).Value = aReleaseDate;

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
        public List<Song> GetSongs()
        {
            List<Song> songs = new List<Song>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT song_id, song_title, duration FROM songs", connection))
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