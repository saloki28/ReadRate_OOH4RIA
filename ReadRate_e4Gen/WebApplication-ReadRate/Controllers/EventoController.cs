using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class EventoController : BasicController
    {
        // GET: EventoController
        public ActionResult Index()
        {
            SessionInitialize();
            EventoRepository eventoRepository = new EventoRepository(session);
            EventoCEN eventoCEN = new EventoCEN(eventoRepository);

            IList<EventoEN> listaEventosEN = eventoCEN.DameTodosEventos(0, -1);

            IEnumerable<EventoViewModel> listEventos = new EventoAssembler().ConvertirListENToViewModel(listaEventosEN).ToList();

            SessionClose();

            return View(listEventos);
        }

        // GET: EventoController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "ID de evento no válido";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                SessionInitialize();
                EventoRepository eventoRepository = new EventoRepository(session);
                EventoCEN eventoCEN = new EventoCEN(eventoRepository);

                EventoEN eventoEN = eventoCEN.DameEventoPorOID(id);
                
                if (eventoEN == null)
                {
                    SessionClose();
                    TempData["Error"] = $"No se encontró el evento con ID {id}";
                    return RedirectToAction(nameof(Index));
                }

                EventoViewModel eventoVM = new EventoAssembler().ConvertirENToViewModel(eventoEN);

                SessionClose();
                return View(eventoVM);
            }
            catch (Exception ex)
            {
                SessionClose();
                TempData["Error"] = "Error al cargar el evento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: EventoController/Create
        public ActionResult Create()
        {
            try
            {
                AdministradorRepository adminRepository = new AdministradorRepository();
                AdministradorCEN administradorCEN = new AdministradorCEN(adminRepository);

                IList<AdministradorEN> listaAdminsEN = administradorCEN.DameTodosAdministradores(0, -1);

                IList<SelectListItem> adminItems = new List<SelectListItem>();

                foreach (AdministradorEN adminen in listaAdminsEN)
                {
                    adminItems.Add(new SelectListItem
                    {
                        Value = adminen.Id.ToString(),
                        Text = adminen.Nombre
                    });
                }

                ViewData["Administradores"] = adminItems;

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar administradores: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: EventoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventoViewModel eventoVM)
        {
            try
            {
                ModelState.Remove("Foto");
                ModelState.Remove("FotoFichero");

                if (ModelState.IsValid)
                {
                    // Validación de AdminPublicadorId
                    if (!eventoVM.AdminPublicadorID.HasValue || eventoVM.AdminPublicadorID.Value <= 0)
                    {
                        ModelState.AddModelError("AdminPublicadorID", "Ha habido un error, el administrador no existe o tiene ID incorrecto");
                        RecargarAdministradores();
                        return View(eventoVM);
                    }

                    // VALIDACIÓN: Aforo actual no puede ser mayor que aforo máximo
                    if (eventoVM.AforoActual.HasValue && eventoVM.AforoMaximo.HasValue && 
                        eventoVM.AforoActual.Value > eventoVM.AforoMaximo.Value)
                    {
                        ModelState.AddModelError("AforoActual", $"El aforo actual ({eventoVM.AforoActual.Value}) no puede ser mayor que el aforo máximo ({eventoVM.AforoMaximo.Value})");
                        RecargarAdministradores();
                        return View(eventoVM);
                    }

                    // MANEJO DEL ARCHIVO DE FOTO
                    if (eventoVM.FotoFichero != null && eventoVM.FotoFichero.Length > 0)
                    {
                        var fileName = Path.GetFileName(eventoVM.FotoFichero.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenEvento", fileName);

                        // Crear directorio si no existe
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenEvento"));

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await eventoVM.FotoFichero.CopyToAsync(stream);
                        }

                        eventoVM.Foto = "/images/imagenEvento/" + fileName;
                    }
                    else
                    {
                        eventoVM.Foto = "/images/imagenEvento/eventoDefault.webp";
                    }

                    // Verificar que el administrador existe antes de crear el evento
                    AdministradorRepository adminRepo = new AdministradorRepository();
                    AdministradorCEN adminCEN = new AdministradorCEN(adminRepo);
                    AdministradorEN adminEN = adminCEN.DameAdministradorPorOID(eventoVM.AdminPublicadorID.Value);
                    
                    if (adminEN == null)
                    {
                        ModelState.AddModelError("AdminPublicadorID", $"El administrador con ID {eventoVM.AdminPublicadorID.Value} no existe en la base de datos");
                        RecargarAdministradores();
                        return View(eventoVM);
                    }

                    EventoRepository eventoRepository = new EventoRepository();
                    EventoCEN eventoCEN = new EventoCEN(eventoRepository);
                    
                    int idEvento = eventoCEN.CrearEvento(
                        p_nombre: eventoVM.Nombre,
                        p_foto: eventoVM.Foto,
                        p_descripcion: eventoVM.TextoDescripcion,
                        p_fecha: eventoVM.FechaPublicacion,
                        p_hora: eventoVM.HoraEvento,
                        p_ubicacion: eventoVM.Ubicacion,
                        p_aforoMax: eventoVM.AforoMaximo.Value,
                        p_administradorEventos: eventoVM.AdminPublicadorID.Value,
                        p_aforoActual: eventoVM.AforoActual.Value
                    );
                    
                    return RedirectToAction(nameof(Index));
                }

                RecargarAdministradores();
                return View(eventoVM);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el evento: " + ex.Message + innerMessage);
                RecargarAdministradores();
                return View(eventoVM);
            }
        }

        // Método auxiliar para recargar administradores
        private void RecargarAdministradores()
        {
            try
            {
                AdministradorRepository adminRepository = new AdministradorRepository();
                AdministradorCEN administradorCEN = new AdministradorCEN(adminRepository);
                IList<AdministradorEN> listaAdminsEN = administradorCEN.DameTodosAdministradores(0, -1);
                IList<SelectListItem> adminItems = new List<SelectListItem>();
                
                foreach (AdministradorEN adminen in listaAdminsEN)
                {
                    adminItems.Add(new SelectListItem
                    {
                        Value = adminen.Id.ToString(),
                        Text = adminen.Nombre
                    });
                }
                
                ViewData["Administradores"] = adminItems;
            }
            catch
            {
                ViewData["Administradores"] = new List<SelectListItem>();
            }
        }

        // GET: EventoController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            EventoRepository eventoRepository = new EventoRepository(session);
            EventoCEN eventoCEN = new EventoCEN(eventoRepository);

            EventoEN eventoEN = eventoCEN.DameEventoPorOID(id);
            EventoViewModel eventoVM = new EventoAssembler().ConvertirENToViewModel(eventoEN);

            SessionClose();
            return View(eventoVM);
        }

        // POST: EventoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventoViewModel evento)
        {
            try
            {
                // Remover validación de campos que no vienen en el formulario
                ModelState.Remove("FotoFichero");
                ModelState.Remove("AdminPublicadorID");
                ModelState.Remove("AdminNombre");

                if (!ModelState.IsValid)
                {
                    return View(evento);
                }

                // VALIDACIÓN: Aforo actual no puede ser mayor que aforo máximo
                if (evento.AforoActual.HasValue && evento.AforoMaximo.HasValue &&
                    evento.AforoActual.Value > evento.AforoMaximo.Value)
                {
                    ModelState.AddModelError("AforoActual", $"El aforo actual ({evento.AforoActual.Value}) no puede ser mayor que el aforo máximo ({evento.AforoMaximo.Value})");
                    return View(evento);
                }

                EventoRepository eventoRepository = new EventoRepository();
                EventoCEN eventoCEN = new EventoCEN(eventoRepository);

                // Obtener el evento original para mantener la foto si no se cambia
                EventoEN eventoOriginal = eventoCEN.DameEventoPorOID(id);
                string fotoActual = eventoOriginal.Foto;

                // MANEJO DE LA FOTO 
                if (evento.FotoFichero != null && evento.FotoFichero.Length > 0)
                {
                    var fileName = Path.GetFileName(evento.FotoFichero.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenEvento", fileName);

                    // Crear directorio si no existe
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenEvento"));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await evento.FotoFichero.CopyToAsync(stream);
                    }

                    evento.Foto = "/images/imagenEvento/" + fileName;
                }
                else
                {
                    // Si no se subió una nueva foto, mantener la actual
                    evento.Foto = !string.IsNullOrEmpty(evento.Foto) ? evento.Foto : fotoActual;
                }

                eventoCEN.ModificarEvento(
                    p_Evento_OID: id,
                    p_nombre: evento.Nombre,
                    p_foto: evento.Foto,
                    p_descripcion: evento.TextoDescripcion,
                    p_fecha: evento.FechaPublicacion,
                    p_hora: evento.HoraEvento,
                    p_ubicacion: evento.Ubicacion,
                    p_aforoMax: evento.AforoMaximo.Value,
                    p_aforoActual: evento.AforoActual.Value
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al editar el evento: " + ex.Message + innerMessage);
                return View(evento);
            }
        }

        // GET: EventoController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                SessionInitialize();
                EventoRepository eventoRepository = new EventoRepository(session);
                EventoCEN eventoCEN = new EventoCEN(eventoRepository);

                EventoEN eventoEN = eventoCEN.DameEventoPorOID(id);

                if (eventoEN == null)
                {
                    SessionClose();
                    return RedirectToAction(nameof(Index));
                }

                EventoViewModel eventoVM = new EventoAssembler().ConvertirENToViewModel(eventoEN);

                SessionClose();
                return View(eventoVM);
            }
            catch
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: EventoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (id <= 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                EventoRepository eventoRepository = new EventoRepository();
                EventoCEN eventoCEN = new EventoCEN(eventoRepository);
                eventoCEN.EliminarEvento(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo eliminar el evento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
