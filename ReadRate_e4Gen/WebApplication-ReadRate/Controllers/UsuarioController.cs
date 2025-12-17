using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using WebApplication_ReadRate.Models;
using WebApplication_ReadRate.Models.Assemblers;

namespace WebApplication_ReadRate.Controllers
{
    public class UsuarioController : BasicController
    {
        // GET: UsuarioController/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: UsuarioController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string rol)
        {
            // Validar que se haya seleccionado un rol
            if (string.IsNullOrEmpty(rol))
            {
                ModelState.AddModelError("", "Debes seleccionar un rol");
                return View();
            }

            // Redirigir al Create correspondiente según el rol seleccionado
            if (rol.ToLower() == "lector")
            {
                return RedirectToAction("Create", "Lector");
            }
            else if (rol.ToLower() == "autor")
            {
                return RedirectToAction("Create", "Autor");
            }

            ModelState.AddModelError("", "Rol no válido");
            return View();
        }

        // GET: UsuarioController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        public ActionResult Login(UsuarioViewModel login)
        {
            // PASO 1: Intentar login como Usuario (Lector o Autor)
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);

            string tokenUsuario = usuarioCEN.Login(login.Email, login.Password);
            
            if(tokenUsuario != null)
            {
                // Es un Usuario (Lector o Autor)
                var listaUsuarios = usuarioCEN.DameUsuarioPorEmail(login.Email);
                if (listaUsuarios != null && listaUsuarios.Count > 0)
                {
                    var usuario = listaUsuarios[0];
                    
                    // Guardar información del usuario en sesión
                    HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
                    HttpContext.Session.SetString("UsuarioNombre", usuario.NombreUsuario);
                    HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
                    HttpContext.Session.SetString("UsuarioRol", usuario.Rol.ToString());
                    HttpContext.Session.SetString("Autenticado", "true");
                    
                    // Redireccionar según el rol del usuario
                    string rol = usuario.Rol.ToString().ToLower();
                    switch (rol)
                    {
                        case "lector":
                            return RedirectToAction("Index", "Home");
                        case "autor":
                            return RedirectToAction("Index", "Home");
                        default:
                            return RedirectToAction("Index", "Home");
                    }
                }
            }
            
            // PASO 2: Si no es Usuario, intentar login como Administrador
            AdministradorRepository administradorRepository = new AdministradorRepository();
            AdministradorCEN administradorCEN = new AdministradorCEN(administradorRepository);
            
            string tokenAdmin = administradorCEN.Login(login.Email, login.Password);
            
            if(tokenAdmin != null)
            {
                // Es un Administrador
                var listaAdmins = administradorCEN.DameAdministradoresPorEmail(login.Email);
                if (listaAdmins != null && listaAdmins.Count > 0)
                {
                    var admin = listaAdmins[0];
                    
                    // Guardar información del administrador en sesión
                    HttpContext.Session.SetString("UsuarioEmail", admin.Email);
                    HttpContext.Session.SetString("UsuarioNombre", admin.Nombre);
                    HttpContext.Session.SetInt32("UsuarioId", admin.Id);
                    HttpContext.Session.SetString("UsuarioRol", "administrador");
                    HttpContext.Session.SetString("Autenticado", "true");
                    
                    // Redireccionar a la página de métricas
                    return RedirectToAction("Index", "Administrador");
                }
            }
            
            // PASO 3: Si no es ni Usuario ni Administrador, credenciales incorrectas
            ModelState.AddModelError("", "Email o contraseña incorrectos");
            return View();
        }
        
        // GET: UsuarioController/HomeVisitante
        public ActionResult HomeVisitante()
        {
            // Establecer rol de visitante en sesión
            HttpContext.Session.SetString("UsuarioRol", "visitante");
            return RedirectToAction("Index", "Home");
        }
        
        // GET: UsuarioController/Logout
        public ActionResult Logout()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: UsuarioController/Index
        public ActionResult Index(UsuarioFiltrosViewModel filtros)
        {
            SessionInitialize();
            UsuarioRepository usuarioRepository = new UsuarioRepository(session);
            UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);

            // =================================================================
            // SELECT ROLES: Obtener roles únicos para el filtro
            // =================================================================
            IList<SelectListItem> rolesSelect = new List<SelectListItem>();
            
            try
            {
                rolesSelect.Add(new SelectListItem { Value = "1", Text = "Autor" });
                rolesSelect.Add(new SelectListItem { Value = "2", Text = "Lector" });
            }
            catch
            {
                // En caso de error, la lista queda vacía
            }

            // Convertir el rol de string a enum nullable
            RolUsuarioEnum? rolFiltro = null;
            if (!string.IsNullOrEmpty(filtros.RolFiltro))
            {
                rolFiltro = (RolUsuarioEnum)Enum.Parse(typeof(RolUsuarioEnum), filtros.RolFiltro);
            }

            // ============================================================
            // APLICAR FILTROS USANDO DameUsuarioPorFiltros
            // ============================================================
            IList<UsuarioEN> listaUsuariosEN = usuarioCEN.DameUsuarioPorFiltros(
                p_rol: rolFiltro,
                p_nombre: filtros.NombreFiltro,
                first: 0,
                size: -1
            );

            // Convertir a ViewModels
            IEnumerable<UsuarioViewModel> listUsuarios = new UsuarioAssembler().ConvertirListENToViewModel(listaUsuariosEN);

            // ============================================================
            // CREAR EL VIEWMODEL COMPLETO PARA LA VISTA
            // ============================================================
            var resultado = new UsuarioFiltrosViewModel
            {
                NombreFiltro = filtros.NombreFiltro,
                RolFiltro = filtros.RolFiltro,
                Usuarios = listUsuarios,
                Roles = rolesSelect
            };

            SessionClose();

            return View(resultado);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}