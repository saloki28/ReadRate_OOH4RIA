using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class LibroController : BasicController
    {
        // GET: LibroController
        public ActionResult Index()
        {   
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            IList<LibroEN> listaLibrosEN = libroCEN.DameTodosLibros(0, -1);

            IEnumerable<LibroViewModel> listLibros = new LibroAssembler().ConvertirListENToViewModel(listaLibrosEN).ToList();
            SessionClose();

            return View(listLibros);
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            LibroEN libroEN = libroCEN.DameLibroPorOID(id);
            LibroViewModel libroVM = new LibroAssembler().ConvertirENToViewModel(libroEN);

            SessionClose();
            return View(libroVM);
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibroViewModel libroVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Validación de AutorPublicadorId
                    if (!libroVM.AutorPublicadorId.HasValue || libroVM.AutorPublicadorId.Value <= 0)
                    {
                        ModelState.AddModelError("AutorPublicadorId", "Debe seleccionar un autor válido (ID mayor que 0)");
                        return View(libroVM);
                    }

                    // No usar sesión compartida para operaciones de escritura
                    LibroRepository libroRepository = new LibroRepository();
                    LibroCEN libroCEN = new LibroCEN(libroRepository);
                    
                    // Orden correcto de parámetros según LibroCEN_crearLibro.cs:
                    // (titulo, genero, edadRecomendada, fechaPublicacion, numPags, sinopsis, fotoPortada, autorPublicador, valoracionMedia)
                    libroCEN.CrearLibro(
                        p_titulo: libroVM.Titulo, 
                        p_genero: libroVM.Genero, 
                        p_edadRecomendada: libroVM.EdadRecomendada, 
                        p_fechaPublicacion: libroVM.FechaPublicacion, 
                        p_numPags: libroVM.NumPags, 
                        p_sinopsis: libroVM.Sinopsis, 
                        p_fotoPortada: libroVM.FotoPortada ?? "default.jpg",
                        p_autorPublicador: libroVM.AutorPublicadorId.Value, 
                        p_valoracionMedia: libroVM.ValoracionMedia
                    );
                    
                    return RedirectToAction(nameof(Index));
                }
                return View(libroVM);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al crear el libro: " + ex.Message + innerMessage);
                return View(libroVM);
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            LibroRepository libroRepository = new LibroRepository(session);
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            LibroEN libroEN = libroCEN.DameLibroPorOID(id);
            LibroViewModel libroVM = new LibroAssembler().ConvertirENToViewModel(libroEN);

            SessionClose();
            return View(libroVM);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LibroViewModel libro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // No usar sesión compartida para operaciones de escritura
                    LibroRepository libroRepository = new LibroRepository();
                    LibroCEN libroCEN = new LibroCEN(libroRepository);
                    libroCEN.ModificarLibro(
                        p_Libro_OID: id,
                        p_titulo: libro.Titulo,
                        p_genero: libro.Genero,
                        p_edadRecomendada: libro.EdadRecomendada,
                        p_fechaPublicacion: libro.FechaPublicacion,
                        p_numPags: libro.NumPags,
                        p_sinopsis: libro.Sinopsis,
                        p_fotoPortada: libro.FotoPortada,
                        p_valoracionMedia: libro.ValoracionMedia
                    );
                    return RedirectToAction(nameof(Index));
                }
                return View(libro);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Error al modificar el libro: " + ex.Message + innerMessage);
                return View(libro);
            }
        }

        // GET: LibroController/Delete/5
        public ActionResult Delete(int id)
        {
            LibroRepository libroRepository = new LibroRepository();
            LibroCEN libroCEN = new LibroCEN(libroRepository);

            LibroEN libroEN = libroCEN.DameLibroPorOID(id);
            LibroViewModel libroVM = new LibroAssembler().ConvertirENToViewModel(libroEN);

            return View(libroVM);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                LibroRepository libroRepository = new LibroRepository();
                LibroCEN libroCEN = new LibroCEN(libroRepository);
                libroCEN.EliminarLibro(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                TempData["ErrorMessage"] = "Error al eliminar el libro: " + ex.Message + innerMessage;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
