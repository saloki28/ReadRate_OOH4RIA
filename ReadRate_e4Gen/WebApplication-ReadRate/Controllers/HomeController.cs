using Microsoft.AspNetCore.Mvc;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using System.Diagnostics;
using WebApplication_ReadRate.Models;

namespace WebApplication_ReadRate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            LibroRepository libroRepo = new LibroRepository();
            LibroCEN libroCEN = new LibroCEN(libroRepo);
            IList<LibroEN> listaLibros = libroCEN.DameLibrosOrdenadosFecha();
            
            // Tomar solo los últimos 5 libros
            var ultimos5Libros = listaLibros.Take(5).ToList();
            
            return View(ultimos5Libros);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
