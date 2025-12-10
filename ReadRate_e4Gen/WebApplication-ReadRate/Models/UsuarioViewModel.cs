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
    }
}
