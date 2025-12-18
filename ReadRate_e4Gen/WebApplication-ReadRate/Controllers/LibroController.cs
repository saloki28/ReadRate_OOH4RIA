using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// using ReadRate_e4Gen.ApplicationCore.CEN.Reusing Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.CP;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class LibroController : BasicController
    {
        private readonly IWebHostEnvironment _webHost;

        public LibroController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        // GET: LibroController/Index
        public ActionResult Index(LibroFiltrosViewModel filtros)
        {   
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            // =================================================================
            // SELECT GÉNEROS: Obtener géneros únicos para el filtro
            // =================================================================
            IList<LibroEN> todosLibros = libroCEN.DameTodosLibros(0, -1);
            IList<SelectListItem> generoSelect = new List<SelectListItem>();
            
            try
            {
                // Obtener géneros únicos
                var generosUnicos = todosLibros
                    .Select(l => l.Genero)
                    .Where(g => !string.IsNullOrEmpty(g))
                    .Distinct()
                    .OrderBy(g => g)
                    .ToList();

                // Crear SelectListItems para el dropdown
                foreach (string genero in generosUnicos)
                {
                    generoSelect.Add(new SelectListItem
                    {
                        Value = genero,
                        Text = genero
                    });
                }
            }
            catch
            {
                // En caso de error, la lista queda vacía
            }

            // ============================================================
            // BUSCAR ID DEL AUTOR SI SE PROPORCIONÓ NOMBRE
            //    El formulario recibe un input de texto; el filtro recibe un ID -> Gestión de varios autores con input coincidente
            // ============================================================
            List<int> autoresIds = new List<int>();
            if (!string.IsNullOrWhiteSpace(filtros.NombreAutorFiltro))
            {
                AutorRepository autorRepository = new AutorRepository(session);
                AutorCEN autorCEN = new AutorCEN(autorRepository);

                // Buscar TODOS los autores cuyo nombre contenga el texto (simulando LIKE)
                IList<AutorEN> autoresEncontrados = autorCEN.DameTodosAutores(0, -1)
                    .Where(a => a.NombreUsuario != null &&
                                a.NombreUsuario.Contains(filtros.NombreAutorFiltro, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Guardar todos los IDs encontrados
                autoresIds = autoresEncontrados.Select(a => a.Id).ToList();
            }

            // ============================================================
            // APLICAR FILTROS USANDO DameLibrosPorFiltros
            // ============================================================

            IList<LibroEN> listaLibrosEN = libroCEN.DameLibrosPorFiltros(
                p_genero: filtros.GeneroFiltro,
                p_titulo: filtros.TituloFiltro,
                p_edadRecomendada: filtros.EdadRecomendadaFiltro,
                p_numPags: null, // No se usa en el formulario
                p_valoracionMedia: filtros.ValoracionMediaFiltro,
                p_autor: null, // null para poder filtar por varios autores
                first: 0,
                size: -1
            );

            // ============================================================
            // FILTRAR POR AUTORES: Autor(es) cuyo nombre contiene el input
            // ============================================================
            if (autoresIds.Count > 0)
            {
                listaLibrosEN = listaLibrosEN.Where(libro => libro.AutorPublicador != null && autoresIds.Contains(libro.AutorPublicador.Id)).ToList();
            }

            // Convertir a ViewModels
            IEnumerable<LibroViewModel> listLibros = new LibroAssembler().ConvertirListENToViewModel(listaLibrosEN).ToList();

            // Modelo completo para la vista axtualizada
            var resultado = new LibroFiltrosViewModel
            {
                TituloFiltro = filtros.TituloFiltro,
                GeneroFiltro = filtros.GeneroFiltro,
                EdadRecomendadaFiltro = filtros.EdadRecomendadaFiltro,
                ValoracionMediaFiltro = filtros.ValoracionMediaFiltro,
                NombreAutorFiltro = filtros.NombreAutorFiltro,
                Libros = listLibros,
                Generos = generoSelect
            };

            SessionClose();

            return View(resultado);
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            LibroEN libroEN = libroCEN.DameLibroPorOID(id);
            
            // Forzar la carga del Autor antes de cerrar la sesión
            string? autorFoto = null;
            if (libroEN?.AutorPublicador != null)
            {
                var autorCargado = libroEN.AutorPublicador.Id;
                var nombreAutor = libroEN.AutorPublicador.NombreUsuario;
                autorFoto = libroEN.AutorPublicador.Foto;
            }
            
            LibroViewModel libroVM = new LibroAssembler().ConvertirENToViewModel(libroEN);
            libroVM.AutorFotoUrl = autorFoto;

            // Cargar reseñas del libro
            ReseñaRepository reseñaRepo = new ReseñaRepository(session);
            ReseñaCEN reseñaCEN = new ReseñaCEN(reseñaRepo);
            IList<ReseñaEN> reseñasEN = reseñaCEN.DameTodosReseñas(0, -1)
                .Where(r => r.LibroReseñado?.Id == id).ToList();
            
            libroVM.Resenas = new ReseñaAssembler().ConvertirListENToViewModel(reseñasEN).ToList();

            // Verificar si el usuario es lector y si el libro está en sus listas
            var lectorId = HttpContext.Session.GetInt32("UsuarioId");
            var rolUsuario = HttpContext.Session.GetString("UsuarioRol");
            
            ViewData["EstaEnGuardados"] = false;
            ViewData["EstaEnCurso"] = false;
            ViewData["TieneReseña"] = false;
            
            if (rolUsuario == "lector" && lectorId.HasValue)
            {
                LectorRepository lectorRepo = new LectorRepository(session);
                LectorCEN lectorCEN = new LectorCEN(lectorRepo);
                LectorEN lectorEN = lectorCEN.DameLectorPorOID(lectorId.Value);
                
                if (lectorEN != null)
                {
                    // Usar la función ComprobarSiEstaEnLista para verificar si está en guardados
                    ViewData["EstaEnGuardados"] = lectorCEN.ComprobarSiEstaEnLista(id, lectorEN.LibroLeido);
                    
                    // Usar la función ComprobarSiEstaEnLista para verificar si está en curso
                    ViewData["EstaEnCurso"] = lectorCEN.ComprobarSiEstaEnLista(id, lectorEN.LibroEnCurso);
                    
                    // Verificar si el lector ya tiene una reseña para este libro
                    ViewData["TieneReseña"] = reseñasEN.Any(r => r.LectorValorador?.Id == lectorId.Value);
                }
            }

            SessionClose();
            return View(libroVM);
        }

        // GET: LibroController/Create
        public ActionResult Create(int autorId)
        {
            ViewData["AutorId"] = autorId;
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LibroViewModel libroVM)
        {
            // Nombre del archivo de la portada
            string fotoPortadaFileName = "sinPortada.webp";
            string path = "";

            // Guardar la imagen de la portada si se ha subido un archivo
            if(libroVM.FotoPortada != null && libroVM.FotoPortada.Length > 0)
            {
                // Guardar el archivo en wwwroot/images/portadas
                fotoPortadaFileName = Path.GetFileName(libroVM.FotoPortada.FileName).Trim();

                string directory = _webHost.WebRootPath + "/images/portadasLibros";
                path = Path.Combine((directory), fotoPortadaFileName);

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = System.IO.File.Create(path))
                {
                    await libroVM.FotoPortada.CopyToAsync(stream);
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Añadir el prefijo de la ruta para acceder a la imagen
                    fotoPortadaFileName = "/images/portadasLibros/" + fotoPortadaFileName;

                    // Usar LibroCP en lugar de LibroCEN para manejar la lógica de negocio
                    LibroCP libroCP = new LibroCP(new SessionCPNHibernate());
                    
                    // Orden correcto de parámetros según LibroCP_crearLibro.cs:
                    // (titulo, genero, edadRecomendada, fechaPublicacion, numPags, sinopsis, fotoPortada, autorPublicador, valoracionMedia)
                    libroCP.CrearLibro(
                        p_titulo: libroVM.Titulo, 
                        p_genero: libroVM.Genero, 
                        p_edadRecomendada: libroVM.EdadRecomendada, 
                        p_fechaPublicacion: DateTime.Now, 
                        p_numPags: libroVM.NumPags, 
                        p_sinopsis: libroVM.Sinopsis, 
                        p_fotoPortada: fotoPortadaFileName,
                        p_autorPublicador: libroVM.AutorId, 
                        p_valoracionMedia: libroVM.ValoracionMedia
                    );
                    
                    return RedirectToAction("Index", "Autor");
                }
                return View(libroVM);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el libro: " + ex.Message + innerMessage);
                return View(libroVM);
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            LibroEN libroEN = libroCEN.DameLibroPorOID(id);
            LibroViewModel libroVM = new LibroAssembler().ConvertirENToViewModel(libroEN);

            SessionClose();
            return View(libroVM);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LibroViewModel libro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Usar la foto actual del ViewModel (que viene de la BD)
                    string fotoPortadaFileName = libro.FotoPortadaUrl ?? string.Empty;

                    // Si se subió una nueva foto, procesarla
                    if (libro.FotoPortada != null && libro.FotoPortada.Length > 0)
                    {
                        // Guardar el archivo en wwwroot/images/portadasLibros
                        string nombreArchivo = Path.GetFileName(libro.FotoPortada.FileName).Trim();
                        string directory = _webHost.WebRootPath + "/images/portadasLibros";
                        string path = Path.Combine(directory, nombreArchivo);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var stream = System.IO.File.Create(path))
                        {
                            await libro.FotoPortada.CopyToAsync(stream);
                        }

                        // Actualizar con la nueva ruta
                        fotoPortadaFileName = "/images/portadasLibros/" + nombreArchivo;
                    }

                    // Si no hay foto actual ni nueva, usar la imagen por defecto
                    if (string.IsNullOrEmpty(fotoPortadaFileName))
                    {
                        fotoPortadaFileName = "/images/portadasLibros/sinPortada.webp";
                    }

                    // Modificar el libro con la foto correspondiente
                    LibroRepository libroRepository = new LibroRepository();
                    LibroCEN libroCEN = new LibroCEN(libroRepository);
                    libroCEN.ModificarLibro(
                        p_Libro_OID: id,
                        p_titulo: libro.Titulo,
                        p_genero: libro.Genero,
                        p_edadRecomendada: libro.EdadRecomendada,
                        p_fechaPublicacion: libro.FechaPublicacion,
                        p_numPags: libro.NumPags,
                        p_sinopsis: libro.Sinopsis,
                        p_fotoPortada: fotoPortadaFileName,
                        p_valoracionMedia: libro.ValoracionMedia
                    );
                    return RedirectToAction(nameof(Index));
                }
                return View(libro);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar el libro: " + ex.Message + innerMessage);
                return View(libro);
            }
        }

        // GET: LibroController/Delete/5
        public ActionResult Delete(int id)
        {
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            LibroEN libroEN = libroCEN.DameLibroPorOID(id);
            
            if (libroEN == null)
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            
            // Forzar la carga del Autor antes de cerrar la sesión
            if (libroEN.AutorPublicador != null)
            {
                var autorCargado = libroEN.AutorPublicador.Id;
                var nombreAutor = libroEN.AutorPublicador.NombreUsuario;
            }
            
            LibroViewModel libroVM = new LibroAssembler().ConvertirENToViewModel(libroEN);

            SessionClose();
            return View(libroVM);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {   
                SessionCPNHibernate CPSession = new SessionCPNHibernate();
                CPSession.SessionInitializeTransaction();
                
                var libroRepository = CPSession.UnitRepo.LibroRepository;
                LibroCEN libroCEN = new LibroCEN(libroRepository);
                
                // Obtener el autor antes de eliminar el libro
                LibroEN libro = libroCEN.DameLibroPorOID(id);
                int? autorId = libro?.AutorPublicador?.Id;
                
                // Eliminar el libro (y sus reseñas)
                libroCEN.EliminarLibro(id);
                
                // Recalcular valoración media del autor si existe
                if (autorId.HasValue)
                {
                    var autorRepo = CPSession.UnitRepo.AutorRepository;
                    var autor = autorRepo.ReadOIDDefault(autorId.Value);
                    
                    if (autor != null)
                    {
                        // Obtener todos los libros restantes del autor
                        var librosAutor = libroCEN.DameTodosLibros(0, int.MaxValue)
                                          .Where(l => l.AutorPublicador != null && l.AutorPublicador.Id == autor.Id)
                                          .ToList();

                        // Calcular la valoración media del autor
                        if (librosAutor.Count > 0)
                            autor.ValoracionMedia = (float)librosAutor.Average(l => l.ValoracionMedia);
                        else
                            autor.ValoracionMedia = 0;

                        autorRepo.ModificarAutor(autor);
                    }
                }
                
                CPSession.Commit();
                CPSession.SessionClose();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar el libro: " + ex.Message + innerMessage;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Añadir libro a lista de guardados
        [HttpPost]
        public ActionResult AgregarAGuardados(int libroId)
        {
            try
            {
                var lectorId = HttpContext.Session.GetInt32("UsuarioId");

                LectorCP lectorCP = new LectorCP(new SessionCPNHibernate());
                lectorCP.AsignarLibroListaGuardados(lectorId.Value, new List<int> { libroId });

                return RedirectToAction("Details", new { id = libroId });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al agregar a guardados: " + ex.Message + innerMessage;
                return RedirectToAction("Details", new { id = libroId });
            }
        }

        // POST: Quitar libro de lista de guardados
        [HttpPost]
        public ActionResult QuitarDeGuardados(int libroId, string? returnUrl = null)
        {
            try
            {
                var lectorId = HttpContext.Session.GetInt32("UsuarioId");

                LectorCP lectorCP = new LectorCP(new SessionCPNHibernate());
                lectorCP.DesasignarLibroListaGuardados(lectorId.Value, new List<int> { libroId });

                if (!string.IsNullOrEmpty(returnUrl) && returnUrl == "perfil")
                {
                    return RedirectToAction("Index", "Lector");
                }
                return RedirectToAction("Details", new { id = libroId });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al quitar de guardados: " + ex.Message + innerMessage;
                if (!string.IsNullOrEmpty(returnUrl) && returnUrl == "perfil")
                {
                    return RedirectToAction("Index", "Lector");
                }
                return RedirectToAction("Details", new { id = libroId });
            }
        }

        // POST: Añadir libro a lista en curso
        [HttpPost]
        public ActionResult AgregarAEnCurso(int libroId, string? returnUrl = null)
        {
            try
            {
                var lectorId = HttpContext.Session.GetInt32("UsuarioId");

                LectorCP lectorCP = new LectorCP(new SessionCPNHibernate());
                lectorCP.AsignarLibroListaEnCurso(lectorId.Value, new List<int> { libroId });

                if (!string.IsNullOrEmpty(returnUrl) && returnUrl == "perfil")
                {
                    return RedirectToAction("Index", "Lector");
                }
                return RedirectToAction("Details", new { id = libroId });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al agregar a en curso: " + ex.Message + innerMessage;
                if (!string.IsNullOrEmpty(returnUrl) && returnUrl == "perfil")
                {
                    return RedirectToAction("Index", "Lector");
                }
                return RedirectToAction("Details", new { id = libroId });
            }
        }

        // POST: Quitar libro de lista en curso
        [HttpPost]
        public ActionResult QuitarDeEnCurso(int libroId, string? returnUrl = null)
        {
            try
            {
                var lectorId = HttpContext.Session.GetInt32("UsuarioId");

                LectorCP lectorCP = new LectorCP(new SessionCPNHibernate());
                lectorCP.DesasignarLibroListaEnCurso(lectorId.Value, new List<int> { libroId });

                if (!string.IsNullOrEmpty(returnUrl) && returnUrl == "perfil")
                {
                    return RedirectToAction("Index", "Lector");
                }
                return RedirectToAction("Details", new { id = libroId });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al quitar de en curso: " + ex.Message + innerMessage;
                if (!string.IsNullOrEmpty(returnUrl) && returnUrl == "perfil")
                {
                    return RedirectToAction("Index", "Lector");
                }
                return RedirectToAction("Details", new { id = libroId });
            }
        }
    }
}
