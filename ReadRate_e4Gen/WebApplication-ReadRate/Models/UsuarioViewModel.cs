using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class UsuarioViewModel
    {
        [Display(Prompt= "Escribe tu email", Description= "Email de usuario", Name = "Email")]
        [Required(ErrorMessage = "Debes indicar tu correo electrónico")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string? Email { get; set; }

        [Display(Prompt="Escribe el password del usuario", Description="Password del usuario", Name = "Password")]
        [Required(ErrorMessage = "El password es obligatorio")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        
        public string? DNI { get; set; }

        public int Id { get; set; }

        // Nombre de usuario
        [Display(Prompt = "Nombre de usuario", Description = "Nombre del usuario", Name = "Nombre de Usuario")]
        public string? NombreUsuario { get; set; }

        // Rol del usuario
        [Display(Name = "Rol")]
        public string? Rol { get; set; }

        // Fecha de nacimiento
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        // Ciudad de residencia
        [Display(Name = "Ciudad de Residencia")]
        public string? CiudadResidencia { get; set; }

        // País de residencia
        [Display(Name = "País de Residencia")]
        public string? PaisResidencia { get; set; }

        // URL de la foto de perfil
        [Display(Name = "Foto")]
        public string? FotoUrl { get; set; }

    }
}
