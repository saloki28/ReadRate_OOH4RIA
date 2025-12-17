using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.CP;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;
using System.Linq;

namespace WebApplication_ReadRate.Controllers
{
    public class ClubController : BasicController
    {
        private readonly IWebHostEnvironment _webHost;

        public ClubController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        // GET: ClubController
        public ActionResult Index()
        {
            SessionInitialize();
            ClubRepository clubRepository = new ClubRepository(session);
            ClubCEN clubCen = new ClubCEN(clubRepository);

            IList<ClubEN> listEN = clubCen.DameTodosClubs(0, -1);

            IEnumerable<ClubViewModel> listClub = new ClubAssembler().ConvertirListENToViewModel(listEN).ToList();
            
            // Obtener IDs de clubs a los que el lector está suscrito
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioRol = HttpContext.Session.GetString("UsuarioRol");
            
            if (usuarioId.HasValue && usuarioRol == "lector")
            {
                LectorRepository lectorRepository = new LectorRepository(session);
                LectorCEN lectorCEN = new LectorCEN(lectorRepository);
                LectorEN lectorEN = lectorCEN.DameLectorPorOID(usuarioId.Value);
                
                if (lectorEN != null && lectorEN.ClubSuscritoLector != null)
                {
                    ViewBag.ClubsSuscritos = lectorEN.ClubSuscritoLector.Select(c => c.Id).ToList();
                }
                else
                {
                    ViewBag.ClubsSuscritos = new List<int>();
                }
                
                // Obtener IDs de clubs de los que el lector es propietario
                var clubsPropietario = listEN.Where(c => c.LectorPropietario?.Id == usuarioId.Value).Select(c => c.Id).ToList();
                ViewBag.ClubsPropietario = clubsPropietario;
            }
            else
            {
                ViewBag.ClubsSuscritos = new List<int>();
                ViewBag.ClubsPropietario = new List<int>();
            }
            
            SessionClose();

            return View(listClub);
        }

        // GET: ClubController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            ClubRepository clubRepo = new ClubRepository(session);
            ClubCEN clubCEN = new ClubCEN(clubRepo);

            ClubEN clubEn = clubCEN.DameClubPorOID(id);

            if (clubEn == null)
            {
                SessionClose();
                return NotFound();
            }

            ClubViewModel clubView = new ClubAssembler().ConvertirENToViewModel(clubEn);

            // Cargar el nombre del propietario
            if (clubEn.LectorPropietario != null)
            {
                LectorRepository lectorRepo = new LectorRepository(session);
                LectorCEN lectorCEN = new LectorCEN(lectorRepo);
                LectorEN lectorPropietario = lectorCEN.DameLectorPorOID(clubEn.LectorPropietario.Id);
                if (lectorPropietario != null)
                {
                    clubView.PropietarioNombre = lectorPropietario.NombreUsuario;
                }
            }

            // Cargar lista de miembros
            var miembros = new List<dynamic>();
            
            // Agregar propietario primero si existe
            if (clubEn.LectorPropietario != null)
            {
                LectorRepository lectorRepo = new LectorRepository(session);
                LectorCEN lectorCEN = new LectorCEN(lectorRepo);
                LectorEN lectorPropietario = lectorCEN.DameLectorPorOID(clubEn.LectorPropietario.Id);
                
                if (lectorPropietario != null)
                {
                    miembros.Add(new
                    {
                        Id = lectorPropietario.Id,
                        Nombre = lectorPropietario.NombreUsuario,
                        Foto = lectorPropietario.Foto,
                        EsPropietario = true
                    });
                }
            }
            
            // Agregar resto de miembros (excluyendo al propietario si está en la lista)
            if (clubEn.LectorMiembro != null && clubEn.LectorMiembro.Any())
            {
                foreach (var miembro in clubEn.LectorMiembro)
                {
                    // No agregar al propietario dos veces
                    if (miembro.Id != clubEn.LectorPropietario?.Id)
                    {
                        miembros.Add(new
                        {
                            Id = miembro.Id,
                            Nombre = miembro.NombreUsuario,
                            Foto = miembro.Foto,
                            EsPropietario = false
                        });
                    }
                }
            }
            ViewBag.Miembros = miembros;

            // Obtener el ID del usuario logueado para el foro
            ViewBag.UsuarioId = HttpContext.Session.GetInt32("UsuarioId");

            // Cargar mensajes del club
            MensajeRepository mensajeRepo = new MensajeRepository(session);
            MensajeCEN mensajeCEN = new MensajeCEN(mensajeRepo);
            
            var todosMensajes = mensajeCEN.DameTodosMensajes(0, -1);
            var mensajesDelClub = todosMensajes.Where(m => m.Club?.Id == id).OrderByDescending(m => m.Fecha).ToList();
            
            var mensajesList = new List<dynamic>();
            foreach (var mensaje in mensajesDelClub)
            {
                mensajesList.Add(new
                {
                    Id = mensaje.Id,
                    TextoContenido = mensaje.Texto,
                    FechaHora = mensaje.Fecha ?? DateTime.Now,
                    LectorId = mensaje.Lector?.Id ?? 0,
                    LectorNombre = mensaje.Lector?.NombreUsuario ?? "Usuario",
                    LectorFoto = mensaje.Lector?.Foto
                });
            }
            ViewBag.Mensajes = mensajesList;

            SessionClose();
            return View(clubView);
        }

        // GET: ClubController/Create
        public ActionResult Create()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            
            if (!usuarioId.HasValue)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var model = new ClubViewModel
            {
                PropietarioId = usuarioId.Value,
                Miembros = 1 // El propietario es el primer miembro
            };

            return View(model);
        }

        // POST: ClubController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClubViewModel club)
        {
            // Asegurar que el propietario es el usuario logueado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (!usuarioId.HasValue)
            {
                return RedirectToAction("Login", "Usuario");
            }
            
            club.PropietarioId = usuarioId.Value;
            club.Miembros = 1; // El propietario es el primer miembro
            
            string fotoFileName = "imagenDefault.webp";
            string path = "";

            // Guardar la imagen de la foto si se ha subido un archivo
            if(club.FotoFile != null && club.FotoFile.Length > 0)
            {
                string directory = _webHost.WebRootPath + "/images/imagenClub";
                
                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Generar nombre único si el archivo ya existe
                string baseFileName = Path.GetFileNameWithoutExtension(club.FotoFile.FileName);
                string extension = Path.GetExtension(club.FotoFile.FileName);
                fotoFileName = $"{baseFileName}{extension}";
                path = Path.Combine(directory, fotoFileName);
                
                int counter = 1;
                while (System.IO.File.Exists(path))
                {
                    fotoFileName = $"{baseFileName}_{counter}{extension}";
                    path = Path.Combine(directory, fotoFileName);
                    counter++;
                }

                using (var stream = System.IO.File.Create(path))
                {
                    await club.FotoFile.CopyToAsync(stream);
                }
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(club);
                }

                // Añadir el prefijo de la ruta para acceder a la imagen
                fotoFileName = "/images/imagenClub/" + fotoFileName;

                // Usar SessionCPNHibernate con transacciones como en CreateDB.cs
                SessionCPNHibernate sessionCP = new SessionCPNHibernate();
                sessionCP.SessionInitializeTransaction();

                LectorCEN lectorCen = new LectorCEN(sessionCP.UnitRepo.LectorRepository);
                LectorEN lectorPropietario = lectorCen.DameLectorPorOID(club.PropietarioId);

                if (lectorPropietario == null)
                {
                    sessionCP.SessionClose();
                    ModelState.AddModelError("", "Error: No se encontró el usuario");
                    return View(club);
                }

                ClubCEN clubCen = new ClubCEN(sessionCP.UnitRepo.ClubRepository);

                // Crear el club con todos los parámetros
                int nuevoId = clubCen.CrearClub(
                    club.Nombre,
                    club.Enlace,
                    club.NumeroMax,
                    fotoFileName,
                    club.Descripcion,
                    lectorPropietario,
                    club.Miembros
                );

                // COMMIT de la transacción - esto es lo que faltaba!
                sessionCP.Commit();
                sessionCP.SessionClose();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el club: " + ex.Message + innerMessage);
                
                return View(club);
            }
        }

        // GET: ClubController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            ClubRepository clubRepo = new ClubRepository(session);
            ClubCEN clubCEN = new ClubCEN(clubRepo);

            ClubEN clubEn = clubCEN.DameClubPorOID(id);
            SessionClose();

            if (clubEn == null)
            {
                return NotFound();
            }

            ClubViewModel clubView = new ClubAssembler().ConvertirENToViewModel(clubEn);
            return View(clubView);
        }

        // POST: ClubController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClubViewModel club)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    // Usar la foto actual del ViewModel (que viene de la BD)
                    string fotoFileName = club.FotoUrl ?? string.Empty;

                    // Si se subió una nueva foto, procesarla
                    if (club.FotoFile != null && club.FotoFile.Length > 0)
                    {
                        string nombreArchivo = Path.GetFileName(club.FotoFile.FileName).Trim();
                        string directory = _webHost.WebRootPath + "/images/imagenClub";
                        string path = Path.Combine(directory, nombreArchivo);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var stream = System.IO.File.Create(path))
                        {
                            await club.FotoFile.CopyToAsync(stream);
                        }

                        // Actualizar con la nueva ruta
                        fotoFileName = "/images/imagenClub/" + nombreArchivo;
                    }

                    // Si no hay foto actual ni nueva, usar la imagen por defecto
                    if (string.IsNullOrEmpty(fotoFileName))
                    {
                        fotoFileName = "/images/imagenClub/imagenDefault.webp";
                    }

                    ClubRepository clubRepo = new ClubRepository();
                    ClubCEN clubCEN = new ClubCEN(clubRepo);

                    clubCEN.ModificarClub(id, club.Nombre, club.Enlace, club.NumeroMax, fotoFileName, club.Descripcion, club.Miembros);
                    return RedirectToAction(nameof(Index));
                }
                return View(club);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar el club: " + ex.Message + innerMessage);
                return View(club);
            }
        }

        // GET: ClubController/DeleteClub - Muestra la vista de confirmación
        public ActionResult DeleteClub(int clubId)
        {
            SessionInitialize();
            ClubRepository clubRepository = new ClubRepository(session);
            ClubCEN clubCEN = new ClubCEN(clubRepository);
            ClubEN clubEN = clubCEN.DameClubPorOID(clubId);
            ClubViewModel clubVM = new ClubAssembler().ConvertirENToViewModel(clubEN);
            SessionClose();
            return View(clubVM);
        }

        // GET: ClubController/ConfirmarEliminarClub - Ejecuta la eliminación
        public ActionResult ConfirmarEliminarClub(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                SessionInitialize();
                ClubRepository clubRepository = new ClubRepository(session);
                ClubCEN clubCEN = new ClubCEN(clubRepository);

                ClubEN clubEN = clubCEN.DameClubPorOID(id);

                if (clubEN == null)
                {
                    SessionClose();
                    return RedirectToAction(nameof(Index));
                }

                ClubViewModel clubVM = new ClubAssembler().ConvertirENToViewModel(clubEN);

                SessionClose();
                return View("Delete", clubVM);
            }
            catch
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ClubController/EliminarClub
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EliminarClub(int id, IFormCollection collection)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioRol = HttpContext.Session.GetString("UsuarioRol");

            if (!usuarioId.HasValue || usuarioRol != "lector")
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión como lector para eliminar clubes.";
                return RedirectToAction("Index");
            }

            SessionCPNHibernate sessionCP = null;
            
            try
            {
                // Usar SessionCPNHibernate con transacciones
                sessionCP = new SessionCPNHibernate();
                sessionCP.SessionInitializeTransaction();
                
                // Verificar que el usuario es el propietario del club
                ClubCEN clubCEN = new ClubCEN(sessionCP.UnitRepo.ClubRepository);
                ClubEN clubEN = clubCEN.DameClubPorOID(id);
                
                if (clubEN == null)
                {
                    sessionCP.SessionClose();
                    TempData["ErrorMessage"] = "Club no encontrado.";
                    return RedirectToAction("Index");
                }
                
                if (clubEN.LectorPropietario?.Id != usuarioId.Value)
                {
                    sessionCP.SessionClose();
                    TempData["ErrorMessage"] = "Solo el propietario puede eliminar el club.";
                    return RedirectToAction("Index");
                }
                
                // 1. Eliminar mensajes asociados al club primero
                MensajeCEN mensajeCEN = new MensajeCEN(sessionCP.UnitRepo.MensajeRepository);
                var todosMensajes = mensajeCEN.DameTodosMensajes(0, -1);
                var mensajesDelClub = todosMensajes.Where(m => m.Club?.Id == id).ToList();
                
                foreach (var mensaje in mensajesDelClub)
                {
                    mensajeCEN.EliminarMensaje(mensaje.Id);
                }
                
                // 2. Desuscribir a todos los miembros del club (excepto el propietario)
                var miembrosDelClub = clubEN.LectorMiembro?.ToList() ?? new List<LectorEN>();
                if (miembrosDelClub.Count > 0)
                {
                    foreach (var miembro in miembrosDelClub)
                    {
                        // No desuscribir al propietario, solo a los miembros regulares
                        if (miembro.Id != clubEN.LectorPropietario?.Id)
                        {
                            try
                            {
                                SessionCPNHibernate sessionCPMiembro = new SessionCPNHibernate();
                                LectorCP lectorCP = new LectorCP(sessionCPMiembro);
                                lectorCP.DesuscribirLectorDeClub(miembro.Id, new List<int> { id });
                            }
                            catch { }
                        }
                    }
                }
                
                // 2.5 Limpiar la relación de propietario antes de eliminar
                clubEN.LectorPropietario = null;
                clubCEN.ModificarClub(
                    clubEN.Id,
                    clubEN.Nombre,
                    clubEN.EnlaceDiscord,
                    clubEN.MiembrosMax,
                    clubEN.Foto,
                    clubEN.Descripcion,
                    clubEN.MiembrosActuales
                );
                // 3. Eliminar el club directamente
                clubCEN.EliminarClub(id);
                
                // COMMIT de la transacción
                sessionCP.Commit();
                sessionCP.SessionClose();
                
                TempData["SuccessMessage"] = "Club eliminado correctamente.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // Hacer rollback si hay error
                if (sessionCP != null)
                {
                    try
                    {
                        sessionCP.RollBack();
                        sessionCP.SessionClose();
                    }
                    catch { }
                }
                
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar el club: " + ex.Message + innerMessage;
                return RedirectToAction("Index");
            }
        }

        // GET: ClubController/Suscribirse
        public ActionResult Suscribirse(int id)
        {
            var lectorId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioRol = HttpContext.Session.GetString("UsuarioRol");

            if (!lectorId.HasValue || usuarioRol != "lector")
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión como lector para suscribirte a clubes.";
                return RedirectToAction("Index");
            }

            try
            {
                SessionCPNHibernate sessionCP = new SessionCPNHibernate();
                LectorCP lectorCP = new LectorCP(sessionCP);
                
                IList<int> clubsIds = new List<int> { id };
                lectorCP.SuscribirLectorAClub(lectorId.Value, clubsIds);
                
                TempData["SuccessMessage"] = "Te has suscrito correctamente al club.";
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Error al suscribirte al club. Inténtalo de nuevo.";
            }

            return RedirectToAction("Index");
        }

        // GET: ClubController/DeleteSuscripcion
        public ActionResult DeleteSuscripcion(int clubId)
        {
            SessionInitialize();
            ClubRepository clubRepository = new ClubRepository(session);
            ClubCEN clubCEN = new ClubCEN(clubRepository);
            ClubEN clubEN = clubCEN.DameClubPorOID(clubId);
            ClubViewModel clubVM = new ClubAssembler().ConvertirENToViewModel(clubEN);
            SessionClose();
            return View(clubVM);
        }

        // GET: ClubController/Desuscribirse
        public ActionResult Desuscribirse(int id)
        {
            var lectorId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioRol = HttpContext.Session.GetString("UsuarioRol");

            if (!lectorId.HasValue || usuarioRol != "lector")
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión como lector.";
                return RedirectToAction("Index");
            }

            try
            {
                SessionCPNHibernate sessionCP = new SessionCPNHibernate();
                LectorCP lectorCP = new LectorCP(sessionCP);
                
                IList<int> clubsIds = new List<int> { id };
                lectorCP.DesuscribirLectorDeClub(lectorId.Value, clubsIds);
                
                TempData["SuccessMessage"] = "Te has dado de baja del club correctamente.";
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Error al darte de baja del club. Inténtalo de nuevo.";
            }

            return RedirectToAction("Index");
        }

        // POST: ClubController/EnviarMensaje
        [HttpPost]
        public ActionResult EnviarMensaje(int clubId, string mensaje)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                var usuarioRol = HttpContext.Session.GetString("UsuarioRol");
                
                if (!usuarioId.HasValue || usuarioRol != "lector")
                {
                    TempData["ErrorMessage"] = "Debes estar logueado como lector para enviar mensajes";
                    return RedirectToAction("Details", new { id = clubId });
                }

                if (string.IsNullOrWhiteSpace(mensaje))
                {
                    TempData["ErrorMessage"] = "El mensaje no puede estar vacío";
                    return RedirectToAction("Details", new { id = clubId });
                }

                MensajeRepository mensajeRepo = new MensajeRepository();
                MensajeCEN mensajeCEN = new MensajeCEN(mensajeRepo);
                
                mensajeCEN.CrearMensaje(mensaje, DateTime.Now, usuarioId.Value, clubId);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al enviar mensaje: " + ex.Message;
            }
            
            return RedirectToAction("Details", new { id = clubId });
        }

        // POST: ClubController/EliminarMensaje
        [HttpPost]
        public ActionResult EliminarMensaje(int id, int clubId)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                
                if (!usuarioId.HasValue)
                {
                    TempData["ErrorMessage"] = "Debes estar logueado para eliminar mensajes";
                    return RedirectToAction("Details", new { id = clubId });
                }

                // Verificar que el mensaje pertenece al usuario
                SessionInitialize();
                MensajeRepository mensajeRepo = new MensajeRepository(session);
                MensajeCEN mensajeCEN = new MensajeCEN(mensajeRepo);
                
                MensajeEN mensaje = mensajeCEN.DameMensajePorOID(id);
                
                if (mensaje == null)
                {
                    SessionClose();
                    TempData["ErrorMessage"] = "Mensaje no encontrado";
                    return RedirectToAction("Details", new { id = clubId });
                }

                if (mensaje.Lector?.Id != usuarioId.Value)
                {
                    SessionClose();
                    TempData["ErrorMessage"] = "No puedes eliminar mensajes de otros usuarios";
                    return RedirectToAction("Details", new { id = clubId });
                }

                SessionClose();

                // Eliminar mensaje
                MensajeRepository mensajeRepoDelete = new MensajeRepository();
                MensajeCEN mensajeCENDelete = new MensajeCEN(mensajeRepoDelete);
                mensajeCENDelete.EliminarMensaje(id);

                TempData["SuccessMessage"] = "Mensaje eliminado correctamente";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar mensaje: " + ex.Message;
            }
            
            return RedirectToAction("Details", new { id = clubId });
        }
    }
}
