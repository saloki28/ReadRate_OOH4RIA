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
    public class ReseñaController : BasicController
    {
        // GET: ReseñaController
        public ActionResult Index()
        {
            SessionInitialize();
            ReseñaRepository resRepository = new ReseñaRepository(session);
            ReseñaCEN resCen = new ReseñaCEN(resRepository);

            IList<ReseñaEN> listEN = resCen.DameTodosReseñas(0, -1);

            IEnumerable<ReseñaViewModel> listRes = new ReseñaAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listRes);
        }

        // GET: ReseñaController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            ReseñaRepository resRepo = new ReseñaRepository(session);
            ReseñaCEN resCEN = new ReseñaCEN(resRepo);

            ReseñaEN resEn = resCEN.DameReseñaPorOID(id);
            ReseñaViewModel resView = new ReseñaAssembler().ConvertirENToViewModel(resEn);

            SessionClose();
            return View(resView);
        }

        // GET: ReseñaController/Create
        public ActionResult Create()
        { 
            LibroRepository libRepo = new LibroRepository();
            LibroCEN libCEN = new LibroCEN(libRepo);

            IList<LibroEN> listaLib = libCEN.DameTodosLibros(0, -1);
            IList<SelectListItem> libItems = new List<SelectListItem>();

            foreach(LibroEN libroEn in listaLib)
            {
                libItems.Add(new SelectListItem{ Text = libroEn.Titulo, Value = libroEn.Id.ToString() });
            }

            ViewData["libItems"] = libItems;

            LectorRepository lecRepo = new LectorRepository();
            LectorCEN lecCEN = new LectorCEN(lecRepo);

            IList<LectorEN> lecLib = lecCEN.DameTodosLectores(0, -1);
            IList<SelectListItem> lecItems = new List<SelectListItem>();

            foreach (LectorEN lecEn in lecLib)
            {
                lecItems.Add(new SelectListItem { Text = lecEn.NombreUsuario , Value = lecEn.Id.ToString() });
            }

            ViewData["lecItems"] = lecItems;

            // Establecer la fecha actual en el modelo
            var model = new ReseñaViewModel
            {
                FechaPublicacion = DateTime.Now
            };

            return View(model);
        }

        // POST: ReseñaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReseñaViewModel res)
        {
            try
            {
                // Depuración detallada
                var fechaExacta = DateTime.Now;
                Console.WriteLine($"=== DEPURACIÓN FECHA ===");
                Console.WriteLine($"Fecha exacta del servidor: {fechaExacta}");
                Console.WriteLine($"Fecha exacta (formato completo): {fechaExacta:dd/MM/yyyy HH:mm:ss.fff}");

                res.FechaPublicacion = fechaExacta;
                Console.WriteLine($"Fecha en el ViewModel: {res.FechaPublicacion}");

                ReseñaRepository resRepo = new ReseñaRepository();
                ReseñaCEN resCen = new ReseñaCEN(resRepo);

                // Guardar y recuperar inmediatamente
                var idReseña = resCen.CrearReseña(res.Opinion, res.Valoracion, res.LectorId, res.LibroId, res.FechaPublicacion.Value);
                Console.WriteLine($"ID de reseña creada: {idReseña}");

                // Recuperar para ver qué se guardó
                var reseñaGuardada = resCen.DameReseñaPorOID(idReseña);
                Console.WriteLine($"Fecha recuperada de BD: {reseñaGuardada.Fecha}");
                Console.WriteLine($"Fecha recuperada (formato completo): {reseñaGuardada.Fecha:dd/MM/yyyy HH:mm:ss.fff}");
                Console.WriteLine($"=== FIN DEPURACIÓN ===");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
                return View();
            }
        }

        // GET: ReseñaController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            ReseñaRepository resRepo = new ReseñaRepository(session);
            ReseñaCEN resCEN = new ReseñaCEN(resRepo);

            ReseñaEN resEn = resCEN.DameReseñaPorOID(id);
            ReseñaViewModel resView = new ReseñaAssembler().ConvertirENToViewModel(resEn);

            SessionClose();
            return View(resView);
        }

        // POST: ReseñaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReseñaViewModel res)
        {
            try
            {
                res.FechaPublicacion = DateTime.Now;
                ReseñaRepository resRepo = new ReseñaRepository();
                ReseñaCEN resCen = new ReseñaCEN(resRepo);
                resCen.ModificarReseña(id, res.Opinion, res.Valoracion, res.FechaPublicacion);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReseñaController/Delete/5
        public ActionResult Delete(int id)
        {
            SessionInitialize();
            ReseñaRepository resRepository = new ReseñaRepository(session);
            ReseñaCEN resCEN = new ReseñaCEN(resRepository);

            ReseñaEN resEN = resCEN.DameReseñaPorOID(id);
            ReseñaViewModel resVM = new ReseñaAssembler().ConvertirENToViewModel(resEN);
            SessionClose();
            return View(resVM);
        }

        // POST: ReseñaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {

                ReseñaRepository resRepository = new ReseñaRepository();
                ReseñaCEN resCEN = new ReseñaCEN(resRepository);
                resCEN.EliminarReseña(id);
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
