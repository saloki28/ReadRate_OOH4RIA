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
            var session = NHibernateHelper.OpenSession();
            try
            {
                // Hacer la consulta directamente con la sesión abierta
                var query = session.GetNamedQuery("LibroNHdameLibrosOrdenadosFechaHQL");
                IList<LibroEN> listaLibros = query.List<LibroEN>();
                
                // Tomar solo los últimos 5 libros y forzar la carga del autor
                var ultimos5Libros = listaLibros.Take(5).ToList();
                
                foreach (var libro in ultimos5Libros)
                {
                    var autorNombre = libro.AutorPublicador?.NombreUsuario;
                }
                
                return View(ultimos5Libros);
            }
            finally
            {
                if (session != null && session.IsOpen)
                {
                    session.Close();
                    session.Dispose();
                }
            }
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