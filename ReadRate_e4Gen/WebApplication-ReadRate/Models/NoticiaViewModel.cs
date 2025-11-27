using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class NoticiaViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        // Título de la noticia
        [Display(Prompt = "Escribe el título de la noticia", Description = "Título de la noticia", Name = "Título")]
        [Required(ErrorMessage = "Debes indicar un título para la noticia")]
        [StringLength(maximumLength: 255, ErrorMessage = "El título no puede tener más de 255 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        // Fecha de la noticia
        [Display(Prompt = "Indica la fecha de publicación de la noticia", Description = "Fecha de la noticia", Name = "Fecha de Publicación")]
        [Required(ErrorMessage = "Debes indicar la fecha de publicación")]
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }

        // Contenido de la noticia
        [Display(Prompt = "Escribe el contenido de la noticia", Description = "Contenido de la noticia", Name = "Noticia")]
        [Required(ErrorMessage = "Debes indicar el contenido de la noticia")]
        [StringLength(maximumLength: 255, ErrorMessage = "El contenido sobrepasa el máximo de caractéres")] // REVISAR MAXIMO CARACTERES
        [DataType(DataType.MultilineText)]
        public string TextoContenido { get; set; } = string.Empty;

        // CÓDIGO SELECT --------------------------------------------------------------------------------------------------------------------------------------------------

        // Nombre del Administrador que publica la noticia
        [Display(Prompt = "Nombre del Administrador", Description = "Nombre del Admin", Name = "Nombre Admin Noticia")]
        public string? AdminNombre { get; set; }

        // Id del Administrador que publica la noticia
        [ScaffoldColumn(false)]
        public int? AdminPublicadorID { get; set; }

        // CÓDIGO FOTO -----------------------------------------------------------------------------------------------------------------------------------------------------

        // Nombre del archivo de la foto de la noticia
        [Display(Prompt = "Escribe el nombre del archivo de la foto de la noticia", Description = "Nombre de la foto de la noticia", Name = "Foto Noticia")]
        [StringLength(maximumLength: 255, ErrorMessage = "El nombre de la foto no puede tener más de 255 caracteres")]
        // La foto es opcional (?
        public string? Foto { get; set; }

        // Nueva propiedad para subir la foto 
        [Display(Name = "Foto de portada", Description = "Sube una imagen para la noticia")]
        public IFormFile? FotoFichero { get; set; }

    }
}
