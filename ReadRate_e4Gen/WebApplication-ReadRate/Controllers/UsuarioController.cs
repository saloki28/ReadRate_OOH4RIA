using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4;
using WebApplication_ReadRate.Models;

namespace WebApplication_ReadRate.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        public ActionResult Login(UsuarioViewModel login)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);

            string token = usuarioCEN.Login(login.Email, login.Password);
            
            if(token == null){
                ModelState.AddModelError("", "Email o contraseña incorrectos");
                return View();
            }
            else{
                // Obtener el usuario completo para conocer su rol
                var listaUsuarios = usuarioCEN.DameUsuarioPorEmail(login.Email);
                if (listaUsuarios != null && listaUsuarios.Count > 0)
                {
                    var usuario = listaUsuarios[0];
                    
                    // Guardar información del usuario en sesión
                    HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
                    HttpContext.Session.SetString("UsuarioNombre", usuario.NombreUsuario);
                    HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
                    HttpContext.Session.SetString("UsuarioRol", usuario.Rol.ToString());
                }
                
                return RedirectToAction("Index", "Home");
            }
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
            return RedirectToAction("Login", "Usuario");
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
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