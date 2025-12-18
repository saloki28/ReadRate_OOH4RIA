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
using System.Net;
using System.Net.Mail;

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
            
            if (resEn == null)
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            
            ReseñaViewModel resView = new ReseñaAssembler().ConvertirENToViewModel(resEn);

            SessionClose();
            return View(resView);
        }

        // GET: ReseñaController/Create
        public ActionResult Create(int libroId, int lectorId)
        {
            
            LibroRepository libroRepo = new LibroRepository();
            LibroCEN libroCEN = new LibroCEN(libroRepo);
            LibroEN libro = libroCEN.DameLibroPorOID(libroId);

            // Establecer la fecha actual en el modelo
            var model = new ReseñaViewModel
            {
                FechaPublicacion = DateTime.Now,
                LibroId = libroId,
                LectorId = lectorId,
                LibroNombre = libro.Titulo
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

                // Usar ReseñaCP para que actualice las valoraciones del libro y autor
                ReseñaCP reseñaCP = new ReseñaCP(new SessionCPNHibernate());

                // Crear la reseña (esto actualiza automáticamente las valoraciones)
                var reseñaCreada = reseñaCP.CrearReseña(
                    p_textoOpinion: res.Opinion, 
                    p_valoracion: res.Valoracion, 
                    p_lectorValorador: res.LectorId, 
                    p_libroReseñado: res.LibroId, 
                    p_fecha: res.FechaPublicacion.Value);
                    
                Console.WriteLine($"ID de reseña creada: {reseñaCreada.Id}");
                Console.WriteLine($"Fecha recuperada de BD: {reseñaCreada.Fecha}");
                Console.WriteLine($"Fecha recuperada (formato completo): {reseñaCreada.Fecha:dd/MM/yyyy HH:mm:ss.fff}");
                Console.WriteLine($"=== FIN DEPURACIÓN ===");
                
                // Obtener datos del autor del libro para enviar email                
                SessionInitialize();                
                LibroRepository libroRepo = new LibroRepository(session);
                LibroCEN libroCEN = new LibroCEN(libroRepo);
                LibroEN libro = libroCEN.DameLibroPorOID(res.LibroId);
                SessionClose();

                // Enviar email al autor
                if (libro != null && libro.AutorPublicador != null && !string.IsNullOrEmpty(libro.AutorPublicador.Email))
                {
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.Credentials = new System.Net.NetworkCredential(
                            "readandrate1@gmail.com",
                            "fyhiyvimlfgtejgl");
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = true;

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("readandrate1@gmail.com", "Read&Rate");
                        mail.To.Add(new MailAddress(libro.AutorPublicador.Email));
                        mail.Subject = $"Nueva reseña para tu libro: {libro.Titulo}";
                        mail.Body = $@"
                        <h2>¡Hola {libro.AutorPublicador.NombreUsuario}!</h2>
                        <p>Tu libro <strong>{libro.Titulo}</strong> ha recibido una nueva reseña.</p>
                        <p><strong>Valoración:</strong> {reseñaCreada.Valoracion} ⭐</p>
                        <p>Visita Read&Rate para ver la reseña completa.</p>";
                        mail.IsBodyHtml = true;

                        smtpClient.Send(mail);
                        Console.WriteLine("Email enviado correctamente");
                    }
                    catch (Exception emailEx)
                    {
                        Console.WriteLine($"[ERROR EMAIL] No se pudo enviar el email: {emailEx.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("No se envió email - Condiciones no cumplidas");
                }
                
                return RedirectToAction("Details", "Libro", new { id = res.LibroId });
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
            
            if (resEn == null)
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            
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
            
            if (resEN == null)
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            
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
                // Obtener el libro antes de eliminar para redirigir correctamente
                ReseñaRepository resRepository = new ReseñaRepository();
                ReseñaCEN resCEN = new ReseñaCEN(resRepository);
                
                ReseñaEN resEN = resCEN.DameReseñaPorOID(id);
                int libroId = resEN.LibroReseñado.Id;
                
                resCEN.EliminarReseña(id);
                
                return RedirectToAction("Details", "Libro", new { id = libroId });
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
