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
    public class LectorController : BasicController
    {
        // GET: LectorController
        public ActionResult Index()
        {
            SessionInitialize();

            // Obtener el ID del usuario de la sesión
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (!usuarioId.HasValue)
            {
                SessionClose();
                return RedirectToAction("Index", "Home");
            }

            LectorRepository lectorRepository = new LectorRepository(session);
            LectorCEN lectorCEN = new LectorCEN(lectorRepository);

            LectorEN lectorEN = lectorCEN.DameLectorPorOID(usuarioId.Value);

            // Forzar la carga de las relaciones antes de cerrar la sesión
            if (lectorEN.LibroEnCurso != null)
            {
                var count1 = lectorEN.LibroEnCurso.Count;
            }
            if (lectorEN.LibroLeido != null)
            {
                var count2 = lectorEN.LibroLeido.Count;
            }
            if (lectorEN.ClubSuscritoLector != null)
            {
                var count3 = lectorEN.ClubSuscritoLector.Count;
                // Forzar la carga de los propietarios de cada club
                foreach (var club in lectorEN.ClubSuscritoLector)
                {
                    if (club.LectorPropietario != null)
                    {
                        var propietarioId = club.LectorPropietario.Id;
                    }
                }
            }

            // Forzar la carga de los clubes creados por el lector
            if (lectorEN.ClubCreado != null)
            {
                var count4 = lectorEN.ClubCreado.Count;
                // Forzar la carga de los propietarios de cada club creado
                foreach (var club in lectorEN.ClubCreado)
                {
                    if (club.LectorPropietario != null)
                    {
                        var propietarioId = club.LectorPropietario.Id;
                    }
                }
            }

            LectorViewModel lectorViewModel = new LectorAssembler().ConvertirENToViewModel(lectorEN);

            SessionClose();

            return View(lectorViewModel);
        }

        // GET: LectorController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            LectorRepository lectorRepository = new LectorRepository(session);
            LectorCEN lectorCEN = new LectorCEN(lectorRepository);

            LectorEN lecEN = lectorCEN.DameLectorPorOID(id);
            LectorViewModel lec = new LectorAssembler().ConvertirENToViewModel(lecEN);

            SessionClose();
            return View(lec);
        }

        // GET: LectorController/Create
        public ActionResult Create()
        {
            // Inicializar el ViewModel con valores por defecto
            var lectorViewModel = new LectorViewModel
            {
                Rol = "2", // 2 = lector (según RolUsuarioEnum)
                CantLibrosCurso = 0,
                CantLibrosLeidos = 0,
                CantAutoresSeguidos = 0,
                CantClubsSuscritos = 0
            };

            return View(lectorViewModel);
        }

        // POST: LectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LectorViewModel lec)
        {
            string fotoFileName = "usuarioDefault.webp";
            string path = "";

            // Guardar la imagen de la foto si se ha subido un archivo
            if(lec.FotoFile != null && lec.FotoFile.Length > 0)
            {
                fotoFileName = Path.GetFileName(lec.FotoFile.FileName).Trim();
                string directory = (HttpContext != null && HttpContext.Request != null && HttpContext.Request.PathBase.HasValue)
                    ? AppDomain.CurrentDomain.BaseDirectory + "wwwroot/images/fotosUsuarios"
                    : "wwwroot/images/fotosUsuarios";
                path = Path.Combine(directory, fotoFileName);

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = System.IO.File.Create(path))
                {
                    await lec.FotoFile.CopyToAsync(stream);
                }
            }

            try
            {
                if(ModelState.IsValid)
                {
                    // Añadir el prefijo de la ruta para acceder a la imagen
                    fotoFileName = "/images/fotosUsuarios/" + fotoFileName;

                    LectorRepository lectorRepository = new LectorRepository();
                    LectorCEN lectorCEN = new LectorCEN(lectorRepository);
                    lectorCEN.CrearLector(
                        p_email: lec.Email,
                        p_nombreUsuario: lec.NombreUsuario,
                        p_fechaNacimiento: lec.FechaNacimiento,
                        p_ciudadResidencia: lec.CiudadResidencia,
                        p_paisResidencia: lec.PaisResidencia,
                        p_foto: fotoFileName,
                        p_rol: (RolUsuarioEnum)Enum.Parse(typeof(RolUsuarioEnum), lec.Rol),
                        p_pass: lec.Pass,
                        p_cantLibrosCurso: lec.CantLibrosCurso,
                        p_cantLibrosLeidos: lec.CantLibrosLeidos,
                        p_cantAutoresSeguidos: lec.CantAutoresSeguidos,
                        p_cantClubsSuscritos: lec.CantClubsSuscritos
                    );

                    // Redirigir a la página de login después del registro
                    return RedirectToAction("Login", "Usuario");
                }
                return View(lec);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el lector: " + ex.Message + innerMessage);
                return View(lec);
            }
        }

        // GET: LectorController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            LectorRepository lectorRepository = new LectorRepository(session);
            LectorCEN lectorCEN = new LectorCEN(lectorRepository);

            LectorEN lecEN = lectorCEN.DameLectorPorOID(id);
            LectorViewModel lec = new LectorAssembler().ConvertirENToViewModel(lecEN);

            SessionClose();
            return View(lec);
        }

        // POST: LectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LectorViewModel lector)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    // Obtener el lector actual para preservar datos
                    SessionInitialize();
                    LectorRepository lectorRepositoryRead = new LectorRepository(session);
                    LectorCEN lectorCENRead = new LectorCEN(lectorRepositoryRead);
                    LectorEN lectorActual = lectorCENRead.DameLectorPorOID(id);
                    SessionClose();

                    // Preservo foto actual
                    string fotoFileName = lector.FotoUrl ?? lectorActual.Foto;

                    // Si se subió una nueva foto, procesarla
                    if (lector.FotoFile != null && lector.FotoFile.Length > 0)
                    {
                        string nombreArchivo = Path.GetFileName(lector.FotoFile.FileName).Trim();
                        string directory = (HttpContext != null && HttpContext.Request != null && HttpContext.Request.PathBase.HasValue)
                            ? AppDomain.CurrentDomain.BaseDirectory + "wwwroot/images/fotosUsuarios"
                            : "wwwroot/images/fotosUsuarios";
                        string path = Path.Combine(directory, nombreArchivo);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var stream = System.IO.File.Create(path))
                        {
                            await lector.FotoFile.CopyToAsync(stream);
                        }

                        // Actualizar con la nueva ruta
                        fotoFileName = "/images/fotosUsuarios/" + nombreArchivo;
                    }

                    // Si no hay foto actual ni nueva, usar la imagen por defecto
                    if (string.IsNullOrEmpty(fotoFileName))
                    {
                        fotoFileName = "/images/fotosUsuarios/usuarioDefault.webp";
                    }

                    string passwordFinal = string.IsNullOrWhiteSpace(lector.Pass)
                        ? lectorActual.Pass  // Mantener contraseña actual (ya hasheada)
                        : lector.Pass;        // Usar nueva contraseña (se hasheara)

                    // Modificar el lector con la foto correspondiente
                    LectorRepository lectorRepository = new LectorRepository();
                    LectorCEN lectorCEN = new LectorCEN(lectorRepository);
                    lectorCEN.ModificarLector(
                        p_Lector_OID: id,
                        p_email: lector.Email,
                        p_nombreUsuario: lector.NombreUsuario,
                        p_fechaNacimiento: lector.FechaNacimiento,
                        p_ciudadResidencia: lector.CiudadResidencia,
                        p_paisResidencia: lector.PaisResidencia,
                        p_foto: fotoFileName,
                        p_rol: (RolUsuarioEnum)Enum.Parse(typeof(RolUsuarioEnum), lector.Rol),
                        p_pass: passwordFinal,
                        p_cantLibrosCurso: lectorActual.CantLibrosCurso,
                        p_cantLibrosLeidos: lectorActual.CantLibrosLeidos,
                        p_cantAutoresSeguidos: lectorActual.CantAutoresSeguidos,
                        p_cantClubsSuscritos: lectorActual.CantClubsSuscritos 
                    );
                    return RedirectToAction(nameof(Index));
                }
                return View(lector);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar el lector: " + ex.Message + innerMessage);
                return View(lector);
            }
        }

        // GET: LectorController/Delete/5
        public ActionResult Delete(int id)
        {
            SessionInitialize();
            LectorRepository lectorRepository = new LectorRepository(session);
            LectorCEN lectorCEN = new LectorCEN(lectorRepository);

            LectorEN lecEN = lectorCEN.DameLectorPorOID(id);
            LectorViewModel lec = new LectorAssembler().ConvertirENToViewModel(lecEN);

            SessionClose();

            return View(lec);
        }

        // POST: LectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Validación simple del ID
                if (id <= 0)
                {
                    TempData["ErrorMessage"] = "ID de lector no válido";
                    return RedirectToAction("Index", "Usuario");
                }

                // Obtener el ID del usuario actual desde la sesión (Admin vs Lector)
                var usuarioActualId = HttpContext.Session.GetInt32("UsuarioId");

                // Eliminar usando LectorCP_eliminarLector
                SessionCPNHibernate sessionCP = new SessionCPNHibernate();
                LectorCP lectorCP = new LectorCP(sessionCP);
                lectorCP.EliminarLector(id);

                // Si el usuario actual es el mismo que se elimina, cerrar sesión
                if (usuarioActualId.HasValue && usuarioActualId.Value == id)
                {
                    HttpContext.Session.Clear();

                    TempData["SuccessMessage"] = "Tu cuenta ha sido eliminada correctamente";
                    return RedirectToAction("Index", "Home");
                }
                else // Si es un admin eliminando otro lector
                {
                    TempData["SuccessMessage"] = "Lector eliminado correctamente";
                    return RedirectToAction("Index", "Usuario");
                }
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar el lector: " + ex.Message + innerMessage;
                return RedirectToAction("Index", "Usuario");
            }
        }
    }
}
