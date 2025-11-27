using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
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
            AdministradorRepository adminRepository = new AdministradorRepository(session);
            AdministradorCEN adminCen = new AdministradorCEN(adminRepository);

            IList<AdministradorEN> listEN = adminCen.DameTodosAdministradores(0,-1);

            IEnumerable<AdministradorViewModel> listRes = new AdministradorAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listRes);
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
