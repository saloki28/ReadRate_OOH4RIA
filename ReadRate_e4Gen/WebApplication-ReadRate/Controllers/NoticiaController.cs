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
    public class NoticiaController : BasicController
    {
        // GET: NoticiaController
        public ActionResult Index()
        {
            SessionInitialize();
            NoticiaRepository noticiaRepository = new NoticiaRepository(session);
            NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);

            IList<NoticiaEN> listaNoticiasEN = noticiaCEN.DameTodosNoticias(0, -1);

            IEnumerable<NoticiaViewModel> listNoticias = new NoticiaAssembler().ConvertirListENToViewModel(listaNoticiasEN).ToList();

            SessionClose();

            return View(listNoticias);
        }

        // GET: NoticiaController/Details/5
        public ActionResult Details(int id)
        {
            //  Validar que el ID sea válido
            if (id <= 0)
            {
                TempData["Error"] = "ID de noticia no válido";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                SessionInitialize();
                NoticiaRepository noticiaRepository = new NoticiaRepository(session);
                NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);

                NoticiaEN noticiaEN = noticiaCEN.DameNoticiaPorOID(id);
                
                //  VALIDACIÓN: Si la noticia no existe, redirigir
                if (noticiaEN == null)
                {
                    SessionClose();
                    TempData["Error"] = $"No se encontró la noticia con ID {id}";
                    return RedirectToAction(nameof(Index));
                }

                NoticiaViewModel noticiaVM = new NoticiaAssembler().ConvertirENToViewModel(noticiaEN);

                SessionClose();
                return View(noticiaVM);
            }
            catch (Exception ex)
            {
                SessionClose();
                TempData["Error"] = "Error al cargar la noticia: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NoticiaController/Create
        public ActionResult Create()
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

            // Precargar fecha actual
            NoticiaViewModel viewModelPreCargado = new NoticiaViewModel();
            viewModelPreCargado.FechaPublicacion = DateTime.Now;

            return View(viewModelPreCargado);
        }

        // POST: NoticiaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoticiaViewModel noticiaVM)
        {
            try
            {
                ModelState.Remove("FechaPublicacion");
                ModelState.Remove("Foto");
                ModelState.Remove("FotoFichero");

                if (ModelState.IsValid)
                {
                    // Validación de AdminPublicadorId
                    if (!noticiaVM.AdminPublicadorID.HasValue || noticiaVM.AdminPublicadorID.Value <= 0)
                    {
                        ModelState.AddModelError("AdminPublicadorID", "Ha habido un error, el administrador no existe o tiene ID incorrecto");
                        RecargarAdministradores();
                        return View(noticiaVM);
                    }

                    // Manejo del archivo de foto
                    if (noticiaVM.FotoFichero != null && noticiaVM.FotoFichero.Length > 0)
                    {
                        var fileName = Path.GetFileName(noticiaVM.FotoFichero.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenNoticia", fileName);

                        // Crear directorio si no existe
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenNoticia"));

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await noticiaVM.FotoFichero.CopyToAsync(stream);
                        }

                        noticiaVM.Foto = "/images/imagenNoticia/" + fileName;
                    }
                    else
                    {
                        noticiaVM.Foto = "/images/imagenNoticia/noticiaDefault.webp"; // Foto por defecto si no se sube ninguna
                    }

                    NoticiaRepository noticiaRepository = new NoticiaRepository();
                    NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);

                    noticiaCEN.CrearNoticia(
                        p_titulo: noticiaVM.Titulo,
                        p_fechaPublicacion: DateTime.Now,
                        p_foto: noticiaVM.Foto,
                        p_textoContenido: noticiaVM.TextoContenido,
                        p_administradorNoticias: noticiaVM.AdminPublicadorID.Value
                    );

                    return RedirectToAction(nameof(Index));
                }

                RecargarAdministradores();
                return View(noticiaVM);
            }
            catch(Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear la noticia: " + ex.Message + innerMessage);
                RecargarAdministradores();
                return View(noticiaVM);
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

        // GET: NoticiaController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            NoticiaRepository noticiaRepository = new NoticiaRepository(session);
            NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);

            NoticiaEN noticiaEN = noticiaCEN.DameNoticiaPorOID(id);
            NoticiaViewModel noticiaVM = new NoticiaAssembler().ConvertirENToViewModel(noticiaEN);

            SessionClose();
            return View(noticiaVM);
        }

        // POST: NoticiaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, NoticiaViewModel noticiaVM)
        {
            try
            {
                NoticiaRepository noticiaRepository = new NoticiaRepository();
                NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);

                NoticiaEN noticiaOriginal = noticiaCEN.DameNoticiaPorOID(id);
                string fotoActual = noticiaOriginal.Foto;

                // Manejo de la foto: si sube una nueva, la procesamos; si no, mantenemos la actual
                if (noticiaVM.FotoFichero != null && noticiaVM.FotoFichero.Length > 0)
                {
                    var fileName = Path.GetFileName(noticiaVM.FotoFichero.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenNoticia", fileName);

                    // Crear directorio si no existe
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/imagenNoticia"));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await noticiaVM.FotoFichero.CopyToAsync(stream);
                    }

                    noticiaVM.Foto = "/images/imagenNoticia/" + fileName;
                }
                else
                {
                    // Si no se subió una nueva foto, mantener la actual
                    noticiaVM.Foto = fotoActual;
                }

                noticiaCEN.ModificarNoticia(
                    p_Noticia_OID: id,
                    p_titulo: noticiaVM.Titulo,
                    p_fechaPublicacion: noticiaVM.FechaPublicacion,
                    p_foto: noticiaVM.Foto,
                    p_textoContenido: noticiaVM.TextoContenido
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar la noticia: " + ex.Message + innerMessage);
                return View(noticiaVM);
            }
        }

        // GET: NoticiaController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                SessionInitialize();
                NoticiaRepository noticiaRepository = new NoticiaRepository(session);
                NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);

                NoticiaEN noticiaEN = noticiaCEN.DameNoticiaPorOID(id);
                
                if (noticiaEN == null)
                {
                    SessionClose();
                    return RedirectToAction(nameof(Index));
                }

                NoticiaViewModel noticiaVM = new NoticiaAssembler().ConvertirENToViewModel(noticiaEN);

                SessionClose();
                return View(noticiaVM);
            }
            catch
            {
                SessionClose();
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NoticiaController/Delete/5
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

                NoticiaRepository noticiaRepository = new NoticiaRepository();
                NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);
                noticiaCEN.EliminarNoticia(id);
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo eliminar la noticia: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
