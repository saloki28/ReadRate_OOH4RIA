using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class LibroViewModel
    {
        // Id del libro
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        // Título del libro
        [Display(Prompt="Escribe el título del libro", Description="Título del libro", Name = "Título")]
        [Required(ErrorMessage = "Debes indicar un título para el libro")]
        [StringLength(maximumLength: 200, ErrorMessage = "El título no puede tener más de 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        // Género del libro
        [Display(Prompt="Especifica el género del libro", Description="Género del libro", Name = "Género")]
        [Required(ErrorMessage = "Debes indicar un género para el libro")]
        [StringLength(maximumLength: 50, ErrorMessage = "El género no puede tener más de 50 caracteres")]
        public string Genero { get; set; } = string.Empty;

        // Edad Recomendada del libro
        [Display(Prompt="Especifica la edad recomendada para leer este libro", Description="Edad Recomendada del libro", Name = "Edad Recomendada")]
        [Required(ErrorMessage = "Debes indicar la edad recomendada")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "La edad debe estar entre 0 y 100 años")]
        public int EdadRecomendada { get; set; }

        // Fecha de Publicación del libro
        [Display(Prompt="Indica la fecha de publicación del libro", Description="Fecha de publicación del libro", Name = "Fecha de Publicación")]
        [Required(ErrorMessage = "Debes indicar la fecha de publicación")]
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }

        // Número de Páginas del libro
        [Display(Prompt="Introduce el número de páginas del libro", Description="Número de Páginas del libro", Name = "Número de Páginas")]
        [Required(ErrorMessage = "Debes indicar el número de páginas")]
        [Range(minimum: 1, maximum: 10000, ErrorMessage = "El número de páginas debe estar entre 1 y 10000")]
        public int NumPags { get; set; }

        // Sinopsis del libro
        [Display(Prompt="Escribe la sinopsis del libro", Description="Sinopsis del libro", Name = "Sinopsis")]
        [Required(ErrorMessage = "Debes indicar una sinopsis para el libro")]
        [StringLength(maximumLength: 255, ErrorMessage = "La sinopsis no puede tener más de 255 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Sinopsis { get; set; } = string.Empty;

        // Ruta de la foto de portada guardada en BD (string)
        [ScaffoldColumn(false)]
        public string? FotoPortadaUrl { get; set; }

        // Archivo de la foto de portada para subir (IFormFile)
        [Display(Prompt="Selecciona el archivo de la portada del libro", Description="Archivo de la portada del libro", Name = "Foto de Portada")]
        public IFormFile? FotoPortada { get; set; }
 
        // Valoración media del libro
        [Display(Prompt="Escribe la valoración media del libro", Description="Valoración media del libro", Name = "Valoración Media")]
        [Required(ErrorMessage = "Debes indicar la valoración media del libro")]
        [Range(minimum: 0.0, maximum: 5.0, ErrorMessage = "La valoración debe estar entre 0 y 5")]
        public float ValoracionMedia { get; set; }

        // Nombre del autor del libro
        [Display(Prompt = "Selecciona el nombre del autor del libro", Description = "Nombre del autor del libro", Name = "Autor")]
        public string? NombreAutor { get; set; }

        // Id del autor del libro
        [ScaffoldColumn(false)]
        public int AutorId { get; set; }

        // Autor del libro
        [ScaffoldColumn(false)]
        public AutorEN? Autor { get; set; }
    }
}
