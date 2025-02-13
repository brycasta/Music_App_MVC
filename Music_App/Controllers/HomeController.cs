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
            List<Playlist> playlists = _dbGateway.GetPlaylists();
            List<Genre> genres = _dbGateway.GetGenres();
            ViewBag.Artists = artists;
            ViewBag.Albums = albums;
            ViewBag.Songs = songs;
            ViewBag.Playlists = playlists;
            ViewBag.Genres = genres;
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

            List<Artist> artists = _dbGateway.GetArtists();

            ViewBag.Artists = artists;

            return View("Index");
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

            List<Artist> artists = _dbGateway.GetArtists();

            ViewBag.Artists = artists;

            return View("Index");
        }
        public IActionResult InsertAnAlbumForm()
        {
            List<Artist> artists = _dbGateway.GetArtists();
            List<Genre> genres = _dbGateway.GetGenres();

            ViewBag.Artists = artists;
            ViewBag.Genres = genres;

            return View();
        }
        public IActionResult InsertAnAlbum(string albumName, DateTime releaseDate)
        {
            _dbGateway.InsertAnAlbum(albumName, releaseDate);

            List<Album> albums = _dbGateway.GetAlbums();

            ViewBag.Albums = albums;

            return View("Index");
        }
        public IActionResult InsertAPlaylistForm()
        {
            return View();
        }
        public IActionResult InsertAPlaylist(string playlistName, string description, DateTime createdDate)
        {
            _dbGateway.InsertAPlaylist(playlistName, description, createdDate);

            List<Playlist> playlists = _dbGateway.GetPlaylists();

            ViewBag.Playlists = playlists;

            return View("Index");
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

            List<Playlist> playlists = _dbGateway.GetPlaylists();

            ViewBag.Playlists = playlists;

            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
