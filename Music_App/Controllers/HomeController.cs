using Microsoft.AspNetCore.Mvc;
using Music_App.Models;
using System.Diagnostics;

namespace Music_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBGateway _dbGateway;

        public HomeController(ILogger<HomeController> logger, DBGateway dbGateway)
        {
            _logger = logger;
            _dbGateway = dbGateway;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Collection()
        {
            List<Artist> artists = _dbGateway.GetArtists();
            List<Album> albums = _dbGateway.GetAlbums();
            List<Song> songs = _dbGateway.GetSongs();
           
            List<Genre> genres = _dbGateway.GetGenres();
            ViewBag.Artists = artists;
            ViewBag.Albums = albums;
            ViewBag.Songs = songs;
            ViewBag.Genres = genres;
            return View();
        }

        public IActionResult TuneIn()
        {
            List<Playlist> playlists = _dbGateway.GetPlaylists();
            ViewBag.Playlists = playlists;
            return View();
        }

        //[Route("/InsertAnArtistForm")]
        public IActionResult InsertAnArtistForm()
        {
            return View();
        }
        //[Route("/InsertAnArtist")]
        public IActionResult InsertAnArtist(string artistName, string description)
        {
            _dbGateway.InsertAnArtist(artistName, description);
            return RedirectToAction("Collection"); 
        }

        public IActionResult DeactivateArtist(int artistId) //Action to deactive row from table
        {
            _dbGateway.DeactivateArtist(artistId);
            return RedirectToAction("Collection");
        }

        public IActionResult FilterCollection(string artistId) //Filter written by Bryan Castaneda to filter Artists
        {
            // Get all data
            List<Artist> artists = _dbGateway.GetArtists();
            List<Album> albums = _dbGateway.GetAlbums();
            List<Song> songs = _dbGateway.GetSongs();
            List<Genre> genres = _dbGateway.GetGenres();

            // Only filter if an artist is selected (artistId is not empty or null)
            if (!string.IsNullOrWhiteSpace(artistId) && int.TryParse(artistId, out int selectedArtistId))
            {
                ViewBag.SelectedArtistId = artistId;
                // Filter artists
                artists = artists.Where(a => a.ArtistId == selectedArtistId).ToList();
                // Filter albums
                albums = albums.Where(a => a.ArtistId == selectedArtistId).ToList();
                // Filter songs through albums
                var albumIds = albums.Select(a => a.AlbumId).ToList();
                songs = songs.Where(s => albumIds.Contains(s.AlbumId)).ToList();
            }
            else
            {
                // When "All Artists" is selected, pass all data to ViewBag
                ViewBag.SelectedArtistId = null;  // Clear the selected artist
            }

            // Pass data to ViewBag
            ViewBag.Artists = artists;
            ViewBag.Albums = albums;
            ViewBag.Songs = songs;
            ViewBag.Genres = genres;

            return View("Collection");
        }

        // For getting the rows by Id for updating tables.
        public IActionResult GetArtistById(int anArtistId)
        {
            List<Artist> artists = _dbGateway.GetArtistById(anArtistId);

            ViewBag.Artists = artists;

            return View();
        }
        public IActionResult UpdateAnArtistForm(int anArtistId)
        {
            List<Artist> artists = _dbGateway.GetArtistById(anArtistId);

            ViewBag.Artists = artists;

            return View();
        }
        public IActionResult UpdateAnArtist(int artistId, string artistName, string description)
        {
            _dbGateway.UpdateAnArtist(artistId, artistName, description);

            return RedirectToAction("Collection");
        }
        public IActionResult InsertAnAlbumForm()
        {
            List<Artist> artists = _dbGateway.GetArtists();
            List<Genre> genres = _dbGateway.GetGenres();

            ViewBag.Artists = artists;
            ViewBag.Genres = genres;

            return View();
        }

        public IActionResult InsertAnAlbum(string albumName, DateTime releaseDate, int artistId, int genreId)
        {
            _dbGateway.InsertAnAlbum(albumName, releaseDate, artistId, genreId);
            return RedirectToAction("Collection");
        }

        public IActionResult InsertAPlaylistForm()
        {
            return View();
        }


        public IActionResult FilterByGenre(string genreId) //Bryan Castaneda allows to filter albums and songs by genre
        {
            // Get all data
            List<Artist> artists = _dbGateway.GetArtists();
            List<Album> albums = _dbGateway.GetAlbums();
            List<Song> songs = _dbGateway.GetSongs();
            List<Genre> genres = _dbGateway.GetGenres();

            // If a genre is selected, filter the albums and songs
            if (!string.IsNullOrWhiteSpace(genreId) && int.TryParse(genreId, out int selectedGenreId))
            {
                ViewBag.SelectedGenreId = genreId;
                // Filter albums by genre
                albums = albums.Where(a => a.GenreId == selectedGenreId).ToList();
                // Filter songs through the filtered albums
                var albumIds = albums.Select(a => a.AlbumId).ToList();
                songs = songs.Where(s => albumIds.Contains(s.AlbumId)).ToList();
            }

            // Pass everything to ViewBag
            ViewBag.Artists = artists;
            ViewBag.Albums = albums;
            ViewBag.Songs = songs;
            ViewBag.Genres = genres;

            return View("Collection");
        }


        public IActionResult InsertAPlaylist(string playlistName, string description, DateTime createdDate)
        {
            _dbGateway.InsertAPlaylist(playlistName, description, createdDate);

            return RedirectToAction("TuneIn");
        }
        public IActionResult GetPlaylistById(int aPlaylistId)
        {
            List<Playlist> playlists = _dbGateway.GetPlaylistById(aPlaylistId);

            ViewBag.Playlists = playlists;

            return View();
        }
        public IActionResult UpdateAPlaylistForm(int aPlaylistId)
        {
            List<Playlist> playlists = _dbGateway.GetPlaylistById(aPlaylistId);

            ViewBag.Playlists = playlists;

            return View();
        }
        public IActionResult UpdateAPlaylist(int playlistId, string playlistName, string description, DateTime createdDate)
        {
            _dbGateway.UpdateAPlaylist(playlistId, playlistName, description, createdDate);

            return RedirectToAction("TuneIn");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
