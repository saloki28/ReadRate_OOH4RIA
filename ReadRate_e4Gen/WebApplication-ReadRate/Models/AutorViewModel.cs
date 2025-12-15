using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class AutorViewModel
    {
        [ScaffoldColumn(false)]
        public int IdUsuario { get; set; }

        // Email del autor
        [Display(Prompt = "Introduce el correo electrónico", Description = "Correo electrónico del autor", Name = "Email")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public String Email { get; set; }

        // Nombre de usuario del autor
        [Display(Prompt = "Introduce el nombre de usuario", Description = "Nombre de usuario del autor", Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(maximumLength: 255, ErrorMessage = "El nombre de usuario no puede tener más de 255 caracteres")]
        public String NombreUsuario { get; set; }

        // Edad del autor
        [Display(Prompt = "Introduce la fecha de nacimiento", Description = "Fecha de nacimiento del autor", Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        // Ciudad de residencia del autor
        [Display(Prompt = "Introduce la ciudad de residencia", Description = "Ciudad de residencia del autor", Name = "Ciudad de Residencia")]
        [Required(ErrorMessage = "La ciudad de residencia es obligatoria")]
        [StringLength(maximumLength: 255, ErrorMessage = "La ciudad de residencia no puede tener más de 255 caracteres")]
        public string CiudadResidencia { get; set; }

        // País de residencia del autor
        [Display(Prompt = "Introduce el país de residencia", Description = "País de residencia del autor", Name = "País de Residencia")]
        [Required(ErrorMessage = "El país de residencia es obligatorio")]
        [StringLength(maximumLength: 255, ErrorMessage = "El país de residencia no puede tener más de 255 caracteres")]
        public string PaisResidencia { get; set; }

        // Rol del autor
        [Display(Prompt = "Indica el rol del autor", Description = "Rol del autor en la plataforma", Name = "Rol")]
        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; } = "1"; // Por defecto siempre es autor (1)

        // Valoración media del autor
        [Display(Prompt = "Escribe la valoración media del autor", Description = "Valoración media del autor", Name = "Valoración Media")]
        [Required(ErrorMessage = "Debes indicar la valoración media del autor")]
        [Range(minimum: 0.0, maximum: 5.0, ErrorMessage = "La valoración debe estar entre 0 y 5")]
        public float ValoracionMedia { get; set; }

        // Contraseña del autor
        [Display(Prompt = "Escribe la contraseña del autor", Description = "Contraseña del autor", Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Pass { get; set; } = string.Empty;

        // Número de seguidores del autor
        [Display(Prompt = "Escribe el número de seguidores del autor", Description = "Número de seguidores del autor", Name = "Número de Seguidores")]
        [Required(ErrorMessage = "Debes indicar el número de seguidores del autor")]
        [Range(minimum: 0, maximum: 9999, ErrorMessage = "El número de seguidores no puede ser negativo")]
        public int NumeroSeguidores { get; set; }

        // Cantidad de libros publicados por el autor
        [Display(Prompt = "Escribe la cantidad de libros publicados por el autor", Description = "Cantidad de libros publicados por el autor", Name = "Cantidad de Libros Publicados")]
        [Required(ErrorMessage = "Debes indicar la cantidad de libros publicados por el autor")]
        [Range(minimum: 0, maximum: 9999, ErrorMessage = "La cantidad de libros publicados no puede ser negativa")]
        public int CantidadLibrosPublicados { get; set; }

        // Archivo de foto del autor para subir
        [Display(Name = "Archivo de Foto")]
        public IFormFile? FotoFile { get; set; }

        // URL de la foto actual (para Edit)
        public string? FotoUrl { get; set; }
    }
}
