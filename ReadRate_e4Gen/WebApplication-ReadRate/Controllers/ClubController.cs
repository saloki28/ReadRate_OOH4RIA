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

            SessionClose();
            return View(clubView);
        }

        // GET: ClubController/Create
        public ActionResult Create()
        {
            // Obtener todos los lectores
            LectorRepository lectorRepo = new LectorRepository();
            LectorCEN lectorCEN = new LectorCEN(lectorRepo);

            IList<LectorEN> listaLectores = lectorCEN.DameTodosLectores(0, -1);
            IList<SelectListItem> lectorItems = new List<SelectListItem>();

            foreach (LectorEN lectorEn in listaLectores)
            {
                lectorItems.Add(new SelectListItem
                {
                    Text = $"{lectorEn.NombreUsuario} ({lectorEn.Email})",
                    Value = lectorEn.Id.ToString()
                });
            }

            ViewData["LectorItems"] = lectorItems;

            return View();
        }

        // POST: ClubController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClubViewModel club)
        {
            string fotoFileName = "imagenDefault.webp";
            string path = "";

            // Guardar la imagen de la foto si se ha subido un archivo
            if(club.FotoFile != null && club.FotoFile.Length > 0)
            {
                fotoFileName = Path.GetFileName(club.FotoFile.FileName).Trim();
                string directory = _webHost.WebRootPath + "/images/imagenClub"; 
                path = Path.Combine(directory, fotoFileName);

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
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
                    // Recargar la lista de lectores para el formulario
                    LectorRepository lectorRepoReload = new LectorRepository();
                    LectorCEN lectorCENReload = new LectorCEN(lectorRepoReload);
                    IList<LectorEN> listaLectoresReload = lectorCENReload.DameTodosLectores(0, -1);
                    IList<SelectListItem> lectorItemsReload = new List<SelectListItem>();
                    foreach (LectorEN lectorEn in listaLectoresReload)
                    {
                        lectorItemsReload.Add(new SelectListItem
                        {
                            Text = $"{lectorEn.NombreUsuario} ({lectorEn.Email})",
                            Value = lectorEn.Id.ToString()
                        });
                    }
                    ViewData["LectorItems"] = lectorItemsReload;
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
                    ModelState.AddModelError("PropietarioId", "No se encontró un lector con ese ID");
                    // Recargar la lista de lectores
                    LectorRepository lectorRepoTemp = new LectorRepository();
                    LectorCEN lectorCENTemp = new LectorCEN(lectorRepoTemp);
                    IList<LectorEN> listaLectores = lectorCENTemp.DameTodosLectores(0, -1);
                    IList<SelectListItem> lectorItems = new List<SelectListItem>();
                    foreach (LectorEN lectorEn in listaLectores)
                    {
                        lectorItems.Add(new SelectListItem
                        {
                            Text = $"{lectorEn.NombreUsuario} ({lectorEn.Email})",
                            Value = lectorEn.Id.ToString()
                        });
                    }
                    ViewData["LectorItems"] = lectorItems;
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
                
                // Recargar la lista de lectores para el formulario
                try
                {
                    LectorRepository lectorRepoError = new LectorRepository();
                    LectorCEN lectorCENError = new LectorCEN(lectorRepoError);
                    IList<LectorEN> listaLectoresError = lectorCENError.DameTodosLectores(0, -1);
                    IList<SelectListItem> lectorItemsError = new List<SelectListItem>();
                    foreach (LectorEN lectorEn in listaLectoresError)
                    {
                        lectorItemsError.Add(new SelectListItem
                        {
                            Text = $"{lectorEn.NombreUsuario} ({lectorEn.Email})",
                            Value = lectorEn.Id.ToString()
                        });
                    }
                    ViewData["LectorItems"] = lectorItemsError;
                }
                catch { }
                
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

        // GET: ClubController/Delete/5
        public ActionResult Delete(int id)
        {
            SessionInitialize();
            ClubRepository clubRepository = new ClubRepository(session);
            ClubCEN clubCEN = new ClubCEN(clubRepository);

            ClubEN clubEN = clubCEN.DameClubPorOID(id);
            SessionClose();

            if (clubEN == null)
            {
                return NotFound();
            }

            ClubViewModel clubVM = new ClubAssembler().ConvertirENToViewModel(clubEN);
            return View(clubVM);
        }

        // POST: ClubController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ClubRepository clubRepository = new ClubRepository();
                ClubCEN clubCEN = new ClubCEN(clubRepository);
                clubCEN.EliminarClub(id);
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
