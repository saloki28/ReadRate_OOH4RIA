using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.CP;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class AutorController : BasicController
    {
        private readonly IWebHostEnvironment _webHost;

        public AutorController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        // GET: AutorController
        public ActionResult Index()
        {
            SessionInitialize();
            AutorRepository autorRepository = new AutorRepository(session);
            AutorCEN autorCEN = new AutorCEN(autorRepository);

            IList<AutorEN> autores = autorCEN.DameTodosAutores(0, -1);

            IEnumerable<AutorViewModel> listAut = new AutorAssembler().ConvertirListENToViewModel(autores).ToList();
            SessionClose();

            return View(listAut);
        }

        // GET: AutorController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            AutorRepository autorRepository = new AutorRepository(session);
            AutorCEN autorCEN = new AutorCEN(autorRepository);

            AutorEN autorEN = autorCEN.DameAutorPorOID(id);
            AutorViewModel autorVM = new AutorAssembler().ConvertirENToViewModel(autorEN);

            SessionClose();
            return View(autorVM);
        }

        // GET: AutorController/Create
        public ActionResult Create()
        {
            // Inicializar el ViewModel con valores por defecto
            var autorViewModel = new AutorViewModel
            {
                Rol = "1", // 1 = autor (según RolUsuarioEnum)
                NumeroSeguidores = 0,
                CantidadLibrosPublicados = 0,
                ValoracionMedia = 0.0f,
                FechaNacimiento = DateTime.Today
            };
            return View(autorViewModel);
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AutorViewModel aut)
        {
            // Nombre del archivo de la foto
            string fotoFileName = "usuarioDefault.webp";
            string path = "";

            // Guardar la imagen de la foto si se ha subido un archivo
            if(aut.FotoFile != null && aut.FotoFile.Length > 0)
            {
                // Guardar el archivo en wwwroot/images/fotosUsuarios
                fotoFileName = Path.GetFileName(aut.FotoFile.FileName).Trim();

                string directory = _webHost.WebRootPath + "/images/fotosUsuarios";
                path = Path.Combine((directory), fotoFileName);

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = System.IO.File.Create(path))
                {
                    await aut.FotoFile.CopyToAsync(stream);
                }
            }

            try
            {
                if(ModelState.IsValid)
                {
                    // Añadir el prefijo de la ruta para acceder a la imagen
                    fotoFileName = "/images/fotosUsuarios/" + fotoFileName;

                    AutorRepository autorRepository = new AutorRepository();
                    AutorCEN autorCEN = new AutorCEN(autorRepository);
                    autorCEN.CrearAutor(
                        p_email: aut.Email, 
                        p_nombreUsuario: aut.NombreUsuario, 
                        p_fechaNacimiento: aut.FechaNacimiento, 
                        p_ciudadResidencia: aut.CiudadResidencia, 
                        p_paisResidencia: aut.PaisResidencia, 
                        p_foto: fotoFileName, 
                        p_rol: (RolUsuarioEnum)Enum.Parse(typeof(RolUsuarioEnum), aut.Rol), 
                        p_pass: aut.Pass,
                        p_numModificaciones: 0,
                        p_numeroSeguidores: aut.NumeroSeguidores, 
                        p_cantidadLibrosPublicados: aut.CantidadLibrosPublicados, 
                        p_valoracionMedia: aut.ValoracionMedia
                    );

                    return RedirectToAction(nameof(Index));
                }

                return View(aut);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el autor: " + ex.Message + innerMessage);
                return View(aut);
            }
        }

        // GET: AutorController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            AutorRepository autorRepository = new AutorRepository(session);
            AutorCEN autorCEN = new AutorCEN(autorRepository);

            AutorEN autorEN = autorCEN.DameAutorPorOID(id);
            AutorViewModel autorVM = new AutorAssembler().ConvertirENToViewModel(autorEN);

            SessionClose();
            return View(autorVM);
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AutorViewModel autor)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    // Obtener el autor actual para saber el número de modificaciones
                    SessionInitialize();
                    AutorRepository autorRepositoryRead = new AutorRepository(session);
                    AutorCEN autorCENRead = new AutorCEN(autorRepositoryRead);
                    AutorEN autorActual = autorCENRead.DameAutorPorOID(id);
                    int numModificaciones = autorActual.NumModificaciones;
                    SessionClose();

                    // Usar la foto actual del ViewModel (que viene de la BD)
                    string fotoFileName = autor.FotoUrl ?? string.Empty;

                    // Si se subió una nueva foto, procesarla
                    if (autor.FotoFile != null && autor.FotoFile.Length > 0)
                    {
                        // Guardar el archivo en wwwroot/images/fotosUsuarios
                        string nombreArchivo = Path.GetFileName(autor.FotoFile.FileName).Trim();
                        string directory = _webHost.WebRootPath + "/images/fotosUsuarios";
                        string path = Path.Combine(directory, nombreArchivo);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var stream = System.IO.File.Create(path))
                        {
                            await autor.FotoFile.CopyToAsync(stream);
                        }

                        // Actualizar con la nueva ruta
                        fotoFileName = "/images/fotosUsuarios/" + nombreArchivo;
                    }

                    // Si no hay foto actual ni nueva, usar la imagen por defecto
                    if (string.IsNullOrEmpty(fotoFileName))
                    {
                        fotoFileName = "/images/fotosUsuarios/usuarioDefault.webp";
                    }

                    // Modificar el autor con la foto correspondiente
                    AutorRepository autorRepository = new AutorRepository();
                    AutorCEN autorCEN = new AutorCEN(autorRepository);
                    autorCEN.ModificarAutor(
                        p_Autor_OID: id,
                        p_email: autor.Email,
                        p_nombreUsuario: autor.NombreUsuario,
                        p_fechaNacimiento: autor.FechaNacimiento,
                        p_ciudadResidencia: autor.CiudadResidencia,
                        p_paisResidencia: autor.PaisResidencia,
                        p_foto: fotoFileName,
                        p_rol: (RolUsuarioEnum)Enum.Parse(typeof(RolUsuarioEnum), autor.Rol),
                        p_pass: autor.Pass,
                        p_numModificaciones: numModificaciones + 1,
                        p_numeroSeguidores: autor.NumeroSeguidores,
                        p_cantidadLibrosPublicados: autor.CantidadLibrosPublicados,
                        p_valoracionMedia: autor.ValoracionMedia
                    );
                    
                    return RedirectToAction(nameof(Index));
                }
                return View(autor);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar el autor: " + ex.Message + innerMessage);
                return View(autor);
            }
        }

        // GET: AutorController/Delete/5
        public ActionResult Delete(int id)
        {
            AutorRepository autorRepository = new AutorRepository();
            AutorCEN autorCEN = new AutorCEN(autorRepository);

            AutorEN autorEN = autorCEN.DameAutorPorOID(id);
            AutorViewModel autorVM = new AutorAssembler().ConvertirENToViewModel(autorEN);

            return View(autorVM);
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SessionCPNHibernate sessionCP = new SessionCPNHibernate();
                AutorCP autorCP = new AutorCP(sessionCP);
                autorCP.EliminarAutor(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar el autor: " + ex.Message + innerMessage;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
