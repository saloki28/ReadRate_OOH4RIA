using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class AdministradorViewModel
    {
        //Id del admin
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //Nombre del admin
        [Display(Prompt = "Nombre admin", Description = "Nombre del admin", Name = "Nombre")]
        [Required(ErrorMessage = "Debes incluir un nombre")]
        [StringLength(maximumLength: 200, ErrorMessage = "El nombre no puede tener más de 200 caracteres")]
        public string Nombre { get; set; }

        //Password
        [Display(Prompt = "Contraseña", Description = "Contrasea del admin", Name = "Password")]
        [Required(ErrorMessage = "Debes incluir una contraseña")]
        public string Password { get; set; }

        //Email
        [Display(Prompt = "Email", Description = "Email de l admin", Name = "Email")]
        [Required(ErrorMessage = "Debes incluir un email")]
        public string Email { get; set; }

        // Archivo de foto del administrador para subir
        [Display(Name = "Archivo de Foto")]
        public IFormFile? FotoFile { get; set; }

        // URL de la foto actual (para Edit)
        public string? FotoUrl { get; set; }
    }
}
