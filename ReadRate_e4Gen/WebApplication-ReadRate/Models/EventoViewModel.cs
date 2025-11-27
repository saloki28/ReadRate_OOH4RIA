using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class EventoViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        // Nombre del evento
        [Display(Prompt = "Escribe el nombre del evento", Description = "nombre del evento", Name = "Nombre")]
        [Required(ErrorMessage = "Debes indicar un nombre para el evento")]
        [StringLength(maximumLength: 255, ErrorMessage = "El nombre no puede tener más de 255 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        // Descripcion del evento
        [Display(Prompt = "Escribe la descripción del evento", Description = "Descripción del evento", Name = "Descripción")]
        [Required(ErrorMessage = "Debes indicar la descripción del evento")]
        [StringLength(maximumLength: 255, ErrorMessage = "La descripción sobrepasa el máximo de caracteres")]
        [DataType(DataType.MultilineText)]
        public string TextoDescripcion { get; set; } = string.Empty;

        // Fecha del evento
        [Display(Prompt = "Indica la fecha del evento", Description = "Fecha del evento", Name = "Fecha")]
        [Required(ErrorMessage = "Debes indicar la fecha del evento")]
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }

        // Hora del evento
        [Display(Prompt = "Indica la hora del evento", Description = "Hora del evento", Name = "Hora")]
        [Required(ErrorMessage = "Debes indicar la hora del evento")]
        [DataType(DataType.Time)]
        public DateTime HoraEvento { get; set; }

        // Ubicación del evento
        [Display(Prompt = "Escribe la ubicación del evento", Description = "Ubicación del evento", Name = "Ubicación")]
        [Required(ErrorMessage = "Debes indicar la ubicación del evento")]
        [StringLength(maximumLength: 255, ErrorMessage = "La ubicación no puede tener más de 255 caracteres")]
        public string Ubicacion { get; set; } = string.Empty;

        // Aforo máximo del evento (int)
        [Display(Prompt = "Indica el aforo máximo del evento", Description = "Aforo máximo del evento", Name = "Aforo Máximo")]
        [Required(ErrorMessage = "Debes indicar el aforo máximo del evento")]
        [Range(minimum: 1, maximum: 100000, ErrorMessage = "El aforo debe estar entre 1 y 100000")]
        public int? AforoMaximo { get; set; }

        // Aforo actual del evento (int)
        [Display(Prompt = "Indica el aforo actual del evento", Description = "Aforo actual del evento", Name = "Aforo Actual")]
        [Required(ErrorMessage = "Debes indicar el aforo actual del evento")]
        [Range(minimum: 0, maximum: 100000, ErrorMessage = "El aforo debe estar entre 0 y 100000")]
        public int? AforoActual { get; set; }


        // CÓDIGO SELECT --------------------------------------------------------------------------------------------------------------------------------------------------

        // Nombre del Administrador que publica el evento
        [Display(Prompt = "Nombre del Administrador", Description = "Nombre del Admin", Name = "Organizador")]
        public string? AdminNombre { get; set; }

        // Id del Administrador que publica el evento
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Debes seleccionar un administrador")]
        public int? AdminPublicadorID { get; set; }

        // CÓDIGO FOTO -----------------------------------------------------------------------------------------------------------------------------------------------------

        // Nombre del archivo de la foto del evento
        [Display(Prompt = "Escribe el nombre del archivo de la foto del evento", Description = "Nombre de la foto del evento", Name = "Foto Evento")]
        [StringLength(maximumLength: 255, ErrorMessage = "El nombre de la foto no puede tener más de 255 caracteres")]
        // La foto es opcional
        public string? Foto { get; set; }

        // Nueva propiedad para subir la foto 
        [Display(Name = "Foto de portada", Description = "Sube una imagen para la noticia")]
        public IFormFile? FotoFichero { get; set; }
    }
}
