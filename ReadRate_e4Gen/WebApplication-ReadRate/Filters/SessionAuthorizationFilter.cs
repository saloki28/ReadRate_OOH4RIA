using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication_ReadRate.Filters
{
    /// <summary>
    /// Filtro de autorización basado en sesiones para proteger controladores y acciones.
    /// Verifica que el usuario esté autenticado y, opcionalmente, que tenga un rol específico.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SessionAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Roles permitidos para acceder al recurso. Si está vacío, solo valida autenticación.
        /// </summary>
        public string[]? AllowedRoles { get; set; }

        /// <summary>
        /// Constructor por defecto. Solo valida autenticación.
        /// </summary>
        public SessionAuthorizationAttribute()
        {
        }

        /// <summary>
        /// Constructor con roles permitidos.
        /// </summary>
        /// <param name="allowedRoles">Roles separados por coma (ej: "lector,autor")</param>
        public SessionAuthorizationAttribute(params string[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            
            // Verificar si el usuario está autenticado
            var autenticado = session.GetString("Autenticado");
            
            if (string.IsNullOrEmpty(autenticado) || autenticado != "true")
            {
                // Usuario no autenticado, redirigir a login
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
                return;
            }

            // Si se especificaron roles, verificar que el usuario tenga uno de ellos
            if (AllowedRoles != null && AllowedRoles.Length > 0)
            {
                var usuarioRol = session.GetString("UsuarioRol")?.ToLower();
                
                if (string.IsNullOrEmpty(usuarioRol))
                {
                    // Usuario sin rol, redirigir a acceso denegado
                    context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
                    return;
                }

                // Verificar si el rol del usuario está en los roles permitidos
                var tieneAcceso = AllowedRoles.Any(role => 
                    role.Equals(usuarioRol, StringComparison.OrdinalIgnoreCase));

                if (!tieneAcceso)
                {
                    // Usuario no tiene el rol requerido
                    context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
                    return;
                }
            }

            // Usuario autenticado y con rol correcto (si se especificó)
        }
    }
}
