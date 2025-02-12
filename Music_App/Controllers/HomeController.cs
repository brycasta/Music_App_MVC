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
            ViewBag.Artists = artists;
            ViewBag.Albums = albums;
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
