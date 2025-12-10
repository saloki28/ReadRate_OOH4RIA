using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class LectorViewModel
    {
        [ScaffoldColumn(false)]
        public int IdUsuario { get; set; }

        // Email del lector
        [Display(Prompt = "Introduce el correo electrónico", Description = "Correo electrónico del lector", Name = "Email")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public String Email { get; set; } = string.Empty;

        // Nombre de usuario del lector
        [Display(Prompt = "Introduce el nombre de usuario", Description = "Nombre de usuario del lector", Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(maximumLength: 255, ErrorMessage = "El nombre de usuario no puede tener más de 255 caracteres")]
        public String NombreUsuario { get; set; } = string.Empty;

        // Edad del lector
        [Display(Prompt = "Introduce la fecha de nacimiento", Description = "Fecha de nacimiento del lector", Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        // Ciudad de residencia del lector
        [Display(Prompt = "Introduce la ciudad de residencia", Description = "Ciudad de residencia del lector", Name = "Ciudad de Residencia")]
        [Required(ErrorMessage = "La ciudad de residencia es obligatoria")]
        [StringLength(maximumLength: 255, ErrorMessage = "La ciudad de residencia no puede tener más de 255 caracteres")]
        public string CiudadResidencia { get; set; } = string.Empty;

        // País de residencia del lector
        [Display(Prompt = "Introduce el país de residencia", Description = "País de residencia del lector", Name = "País de Residencia")]
        [Required(ErrorMessage = "El país de residencia es obligatorio")]
        [StringLength(maximumLength: 255, ErrorMessage = "El país de residencia no puede tener más de 255 caracteres")]
        public string PaisResidencia { get; set; } = string.Empty;

        // Archivo de foto del lector para subir
        [Display(Name = "Archivo de Foto")]
        public IFormFile? FotoFile { get; set; }

        // URL de la foto actual (para Edit/Details)
        public string? FotoUrl { get; set; }

        // Rol del lector
        [Display(Prompt = "Indica el rol del lector", Description = "Rol del lector en la plataforma", Name = "Rol")]
        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; } = "2"; // Por defecto siempre es lector (2)

        // Contraseña del lector
        [Display(Prompt = "Escribe la contraseña del lector", Description = "Contraseña del lector", Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Pass { get; set; } = string.Empty;

        // Cantidad de libros en el curso del lector
        [Display(Prompt = "Escribe la cantidad de libros en el curso del lector", Description = "Cantidad de libros en el curso del lector", Name = "Cantidad de Libros en Curso")]
        [Required(ErrorMessage = "Debes indicar la cantidad de libros en el curso del lector")]
        [Range(minimum: 0, maximum: 9999, ErrorMessage = "La cantidad de libros en curso no puede ser negativa")]       
        public int CantLibrosCurso { get; set; }

        // Cantidad de libros leídos por el lector
        [Display(Prompt = "Escribe la cantidad de libros leídos por el lector", Description = "Cantidad de libros leídos por el lector", Name = "Cantidad de Libros Leídos")]
        [Required(ErrorMessage = "Debes indicar la cantidad de libros leídos por el lector")]
        [Range(minimum: 0, maximum: 9999, ErrorMessage = "La cantidad de libros leídos no puede ser negativa")]
        public int CantLibrosLeidos { get; set; }

        // Cantidad de autores seguidos por el lector
        [Display(Prompt = "Escribe la cantidad de autores seguidos por el lector", Description = "Cantidad de autores seguidos por el lector", Name = "Cantidad de Autores Seguidos")]
        [Required(ErrorMessage = "Debes indicar la cantidad de autores seguidos por el lector")]
        [Range(minimum: 0, maximum: 9999, ErrorMessage = "La cantidad de autores seguidos no puede ser negativa")]
        public int CantAutoresSeguidos { get; set; }

        // Cantidad de clubs suscritos por el lector
        [Display(Prompt = "Escribe la cantidad de clubs suscritos por el lector", Description = "Cantidad de clubs suscritos por el lector", Name = "Cantidad de Clubs Suscritos")]
        [Required(ErrorMessage = "Debes indicar la cantidad de clubs suscritos por el lector")]
        [Range(minimum: 0, maximum: 9999, ErrorMessage = "La cantidad de clubs suscritos no puede ser negativa")]
        public int CantClubsSuscritos { get; set; }

        // Listas para el área personal
        public IList<LibroViewModel> LibrosGuardados { get; set; } = new List<LibroViewModel>();
        public IList<LibroViewModel> LecturasGuardadas { get; set; } = new List<LibroViewModel>();
        public IList<ClubViewModel> ClubsInscritos { get; set; } = new List<ClubViewModel>();
    }
}
