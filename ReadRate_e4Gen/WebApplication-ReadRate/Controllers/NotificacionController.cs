using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.CP;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class NotificacionController : BasicController
    {
        // GET: NotificacionController
        public ActionResult Index()
        {
            SessionInitialize();
            NotificacionRepository notificacionRepository = new NotificacionRepository(session);
            NotificacionCEN notificacionCEN = new NotificacionCEN(notificacionRepository);

            IList<NotificacionEN> listaNotificacionesEN = notificacionCEN.DameTodosNotificaciones(0, -1);

            IEnumerable<NotificacionViewModel> listNotificaciones = new NotificacionAssembler().ConvertirListENToViewModel(listaNotificacionesEN).ToList();
            SessionClose();

            return View(listNotificaciones);
        }

        // GET: NotificacionController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            NotificacionRepository notificacionRepository = new NotificacionRepository(session);
            NotificacionCEN notificacionCEN = new NotificacionCEN(notificacionRepository);

            NotificacionEN notificacionEN = notificacionCEN.DameNotificacionPorOID(id);
            NotificacionViewModel notificacionVM = new NotificacionAssembler().ConvertirENToViewModel(notificacionEN);
            
            SessionClose();
            return View(notificacionVM);
        }

        // GET: NotificacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotificacionViewModel notificacion)
        {
            try
            {
                // No usar sesión compartida para operaciones de escritura
                SessionCPNHibernate sessionCP = new SessionCPNHibernate();
                NotificacionCP notificacionCP = new NotificacionCP(sessionCP);
                
                // Parsear el concepto desde el string del ViewModel
                ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum concepto;
                if (!Enum.TryParse(notificacion.Concepto, out concepto))
                {
                    ModelState.AddModelError("Concepto", "Concepto de notificación inválido");
                    return View(notificacion);
                }
                
                // Por ahora, usar 0 como OID_destino (requiere selección del usuario en vista)
                int oidDestino = 0;
                
                notificacionCP.CrearNotificacion(
                    p_fecha: DateTime.Now,
                    p_concepto: concepto,
                    p_OID_destino: oidDestino,
                    p_tituloResumen: notificacion.Titulo,
                    p_textoCuerpo: notificacion.Texto
                );
                
                return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear la notificación: " + ex.Message + innerMessage);
                return View(notificacion);
            }
        }

        // GET: NotificacionController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            NotificacionRepository notificacionRepository = new NotificacionRepository(session);
            NotificacionCEN notificacionCEN = new NotificacionCEN(notificacionRepository);

            NotificacionEN notificacionEN = notificacionCEN.DameNotificacionPorOID(id);
            NotificacionViewModel notificacionVM = new NotificacionAssembler().ConvertirENToViewModel(notificacionEN);
            SessionClose();

            return View(notificacionVM);
        }

        // POST: NotificacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NotificacionViewModel notificacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Modificar la notificacion con la foto correspondiente
                    NotificacionRepository notificacionRepository = new NotificacionRepository();
                    NotificacionCEN notificacionCEN = new NotificacionCEN(notificacionRepository);
                    notificacionCEN.ModificarNotificacion(
                        p_Notificacion_OID: id,
                        p_fecha: notificacionCEN.DameNotificacionPorOID(id).Fecha, // Mantener la fecha original
                        p_concepto: (ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum)Enum.Parse(typeof(ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum), notificacion.Concepto),
                        p_tituloResumen: notificacion.Titulo,
                        p_textoCuerpo: notificacion.Texto
                    );
                    return RedirectToAction(nameof(Index));
                }
                return View(notificacion);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar la notificación: " + ex.Message + innerMessage);
                return View(notificacion);
            }
        }

        // GET: NotificacionController/Delete/5
        public ActionResult Delete(int id)
        {
            SessionInitialize();
            NotificacionRepository notificacionRepository = new NotificacionRepository(session);
            NotificacionCEN notificacionCEN = new NotificacionCEN(notificacionRepository);

            NotificacionEN notificacionEN = notificacionCEN.DameNotificacionPorOID(id);
            
            NotificacionViewModel notificacionVM = new NotificacionAssembler().ConvertirENToViewModel(notificacionEN);

            SessionClose();
            return View(notificacionVM);
        }

        // POST: NotificacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                NotificacionRepository notificacionRepository = new NotificacionRepository();
                NotificacionCEN notificacionCEN = new NotificacionCEN(notificacionRepository);
                notificacionCEN.EliminarNotificacion(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar la notificación: " + ex.Message + innerMessage;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
