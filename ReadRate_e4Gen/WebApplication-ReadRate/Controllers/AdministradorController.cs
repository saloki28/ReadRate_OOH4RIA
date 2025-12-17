using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using ScottPlot;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class AdministradorController : BasicController
    {
        private readonly IWebHostEnvironment _webHost;

        public AdministradorController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        // GET: AdministradorController
        public ActionResult Index()
        {
            SessionInitialize();
            
            // Generar gráfico pie de usuarios por rol ---------------------------------------------------------------------------------------------

            LectorRepository lectorRepo = new LectorRepository(session);
            LectorCEN lectorCEN = new LectorCEN(lectorRepo);
            var lectores = lectorCEN.DameTodosLectores(0, -1);

            AutorRepository autorRepo = new AutorRepository(session);
            AutorCEN autorCEN = new AutorCEN(autorRepo);
            var autores = autorCEN.DameTodosAutores(0, -1);

            AdministradorRepository adminRepo = new AdministradorRepository(session);
            AdministradorCEN adminCEN = new AdministradorCEN(adminRepo);
            var administradores = adminCEN.DameTodosAdministradores(0, -1);

            // Crear el gráfico con ScottPlot
            ScottPlot.Plot myPlot = new();

            List<ScottPlot.PieSlice> slices = new()
            {
                new ScottPlot.PieSlice() { Value = lectores.Count, FillColor = ScottPlot.Color.FromHex("#ff9800"), LegendText = "Lectores (" + lectores.Count + ")" },
                new ScottPlot.PieSlice() { Value = autores.Count, FillColor = ScottPlot.Color.FromHex("#ff6f00"), LegendText = "Autores (" + autores.Count + ")" },
                new ScottPlot.PieSlice() { Value = administradores.Count, FillColor = ScottPlot.Color.FromHex("#ff3d00"), LegendText = "Administradores (" + administradores.Count + ")" }
            };

            var pie = myPlot.Add.Pie(slices);
            
            pie.SliceLabelDistance = 1.3;

            myPlot.Legend.FontSize = 18;
            myPlot.Legend.Alignment = Alignment.UpperCenter;
            myPlot.Legend.OutlineWidth = 0;
            myPlot.Legend.ShadowColor = Colors.Transparent;
            myPlot.ShowLegend();
            
            // hide unnecessary plot components
            myPlot.Axes.Frameless();
            myPlot.HideGrid();
        

            // Guardar el gráfico
            string graficosPath = Path.Combine(_webHost.WebRootPath, "images", "graficos");
            if (!Directory.Exists(graficosPath))
            {
                Directory.CreateDirectory(graficosPath);
            }
            
            string graficoPiePath = Path.Combine(graficosPath, "usuarios-por-rol.png");
            myPlot.SavePng(graficoPiePath, 600, 500);


            // Total de clubes -------------------------------------------------------------------------------------------------------------
            ClubRepository clubRepo = new ClubRepository(session);
            ClubCEN clubCEN = new ClubCEN(clubRepo);
            int totalClubes = clubCEN.DameTodosClubs(0, -1).Count;

            ViewData["TotalClubes"] = totalClubes;


            // Total de libros -------------------------------------------------------------------------------------------------------------
            LibroRepository libroRepo = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepo);
            int totalLibros = libroCEN.DameCantidadTotalLibros();

            ViewData["TotalLibros"] = totalLibros;

            // Libros con más reseñas -------------------------------------------------------------------------------------------------------------
            // Obtener los 5 libros con más reseñas
            var librosConResenas = libroCEN.DameTodosLibros(0, -1)
                .Select(l => new
                {
                    Libro = l,
                    NumResenas = l.Reseña != null ? l.Reseña.Count : 0
                })
                .OrderByDescending(lr => lr.NumResenas)
                .Take(5)
                .ToList();

            ScottPlot.Plot myPlot2 = new();
            
            double[] values = librosConResenas.Select(lr => (double)lr.NumResenas).ToArray(); // Número de reseñas
            double[] positions = Enumerable.Range(0, values.Length).Select(i => (double)i).ToArray(); // Posiciones en el eje X
            
            var bars = myPlot2.Add.Bars(positions, values); // Crear barras
            
            // Configurar color naranja para todas las barras y añadir etiquetas encima
            string[] labels = librosConResenas.Select(lr => lr.Libro.Titulo.Length > 15 
                ? lr.Libro.Titulo.Substring(0, 15) + "..." 
                : lr.Libro.Titulo).ToArray();
            
            for (int i = 0; i < bars.Bars.Count; i++)
            {
                bars.Bars[i].FillColor = ScottPlot.Color.FromHex("#ff6f00");
                bars.Bars[i].Label = labels[i];
                bars.Bars[i].LineWidth = 0;
            }
            
            bars.ValueLabelStyle.Bold = true;
            bars.ValueLabelStyle.FontSize = 12;

            // Configurar ejes
            myPlot2.Axes.Left.Label.Text = "Número de Reseñas";
            myPlot2.Axes.Bottom.Label.Text = "Libros";
            
            // Quitar números del eje X y pegar barras al eje
            myPlot2.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator(); // Sin marcas de graduación
            myPlot2.Axes.SetLimitsY(0, values.Max() * 1.15); // Límite superior un 15% más alto que la barra más alta
            
            // Estilo del gráfico
            myPlot2.HideGrid();
            
            // Guardar el gráfico
            string graficoBarrasPath = Path.Combine(graficosPath, "libros-mas-resenas.png");
            myPlot2.SavePng(graficoBarrasPath, 600, 500);

            // Grafico total clubs, eventos, noticias --------------------------------------------------------------------------------------------- 
            
            EventoRepository eventoRepo = new EventoRepository(session);
            EventoCEN eventoCEN = new EventoCEN(eventoRepo);
            int totalEventos = eventoCEN.DameTodosEventos(0, -1).Count;

            NoticiaRepository noticiaRepo = new NoticiaRepository(session);
            NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepo);
            int totalNoticias = noticiaCEN.DameTodosNoticias(0, -1).Count;

            // Crear el gráfico con ScottPlot
            ScottPlot.Plot myPlot3 = new();

            List<ScottPlot.PieSlice> slices3 = new()
            {
                new ScottPlot.PieSlice() { Value = totalClubes, FillColor = ScottPlot.Color.FromHex("#ff9900ff"), LegendText = "Clubes (" + totalClubes + ")" },
                new ScottPlot.PieSlice() { Value = totalEventos, FillColor = ScottPlot.Color.FromHex("#ff5500ff"), LegendText = "Eventos (" + totalEventos + ")" },
                new ScottPlot.PieSlice() { Value = totalNoticias, FillColor = ScottPlot.Color.FromHex("#7b2100ff"), LegendText = "Noticias (" + totalNoticias + ")" }
            };

            var pie3 = myPlot3.Add.Pie(slices3);
            
            pie3.SliceLabelDistance = 1.3;

            myPlot3.Legend.FontSize = 15;
            myPlot3.Legend.Alignment = Alignment.MiddleRight;
            myPlot3.Legend.OutlineWidth = 0;
            myPlot3.Legend.ShadowColor = Colors.Transparent;
            myPlot3.ShowLegend();
            
            // hide unnecessary plot components
            myPlot3.Axes.Frameless();
            myPlot3.HideGrid();
        

            // Guardar el gráfico
            
            string graficoPiePath2 = Path.Combine(graficosPath, "numeros-totales.png");
            myPlot3.SavePng(graficoPiePath2, 600, 500);

            SessionClose();

            return View();
        }

        // GET: AdministradorController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            AdministradorRepository adminRepo = new AdministradorRepository(session);
            AdministradorCEN adminCEN = new AdministradorCEN(adminRepo);

            AdministradorEN adminEn = adminCEN.DameAdministradorPorOID(id);
            AdministradorViewModel adminView = new AdministradorAssembler().ConvertirENToViewModel(adminEn);

            SessionClose();
            return View(adminView);
        }

        // GET: AdministradorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministradorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdministradorViewModel admin)
        {
            // Nombre del archivo de la foto
            string fotoFileName = "usuarioDefault.webp";
            string path = "";

            // Guardar la imagen de la foto si se ha subido un archivo
            if(admin.FotoFile != null && admin.FotoFile.Length > 0)
            {
                // Guardar el archivo en wwwroot/images/fotosUsuarios
                fotoFileName = Path.GetFileName(admin.FotoFile.FileName).Trim();

                string directory = _webHost.WebRootPath + "/images/fotosUsuarios";
                path = Path.Combine((directory), fotoFileName);

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = System.IO.File.Create(path))
                {
                    await admin.FotoFile.CopyToAsync(stream);
                }
            }

            try
            {
                if(ModelState.IsValid)
                {
                    // Añadir el prefijo de la ruta para acceder a la imagen
                    fotoFileName = "/images/fotosUsuarios/" + fotoFileName;

                    AdministradorRepository adminRepo = new AdministradorRepository();
                    AdministradorCEN adminCen = new AdministradorCEN(adminRepo);
                    adminCen.CrearAdministador(admin.Nombre, admin.Password, admin.Email, fotoFileName);
                    return RedirectToAction(nameof(Index));
                }

                return View(admin);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el administrador: " + ex.Message + innerMessage);
                return View(admin);
            }
        }

        // GET: AdministradorController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            AdministradorRepository adminRepo = new AdministradorRepository(session);
            AdministradorCEN adminCEN = new AdministradorCEN(adminRepo);

            AdministradorEN adminEn = adminCEN.DameAdministradorPorOID(id);
            AdministradorViewModel adminView = new AdministradorAssembler().ConvertirENToViewModel(adminEn);

            SessionClose();
            return View(adminView);
        }

        // POST: AdministradorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AdministradorViewModel admin)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    // Usar la foto actual del ViewModel (que viene de la BD)
                    string fotoFileName = admin.FotoUrl ?? string.Empty;

                    // Si se subió una nueva foto, procesarla
                    if (admin.FotoFile != null && admin.FotoFile.Length > 0)
                    {
                        // Guardar el archivo en wwwroot/images/fotosUsuarios
                        string nombreArchivo = Path.GetFileName(admin.FotoFile.FileName).Trim();
                        string directory = _webHost.WebRootPath + "/images/fotosUsuarios";
                        string path = Path.Combine(directory, nombreArchivo);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var stream = System.IO.File.Create(path))
                        {
                            await admin.FotoFile.CopyToAsync(stream);
                        }

                        // Actualizar con la nueva ruta
                        fotoFileName = "/images/fotosUsuarios/" + nombreArchivo;
                    }

                    // Si no hay foto actual ni nueva, usar la imagen por defecto
                    if (string.IsNullOrEmpty(fotoFileName))
                    {
                        fotoFileName = "/images/fotosUsuarios/usuarioDefault.webp";
                    }

                    AdministradorRepository adminRepo = new AdministradorRepository();
                    AdministradorCEN adminCen = new AdministradorCEN(adminRepo);
                    adminCen.ModificarAdministador(id, admin.Nombre, admin.Password, admin.Email, fotoFileName);

                    return RedirectToAction(nameof(Index));
                }

                return View(admin);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar el administrador: " + ex.Message + innerMessage);
                return View(admin);
            }
        }

        // GET: AdministradorController/Delete/5
        public ActionResult Delete(int id)
        {
            SessionInitialize();
            AdministradorRepository adminRepository = new AdministradorRepository(session);
            AdministradorCEN adminCEN = new AdministradorCEN(adminRepository);

            AdministradorEN adminEN = adminCEN.DameAdministradorPorOID(id);
            AdministradorViewModel adminVM = new AdministradorAssembler().ConvertirENToViewModel(adminEN);
            SessionClose();
            return View(adminVM);
        }

        // POST: AdministradorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                AdministradorRepository adminRepository = new AdministradorRepository();
                AdministradorCEN adminCEN = new AdministradorCEN(adminRepository);
                adminCEN.EliminarAdministador(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar la reseña: " + ex.Message + innerMessage;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
