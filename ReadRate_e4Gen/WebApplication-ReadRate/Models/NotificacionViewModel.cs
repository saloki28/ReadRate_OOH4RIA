using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class NotificacionViewModel
    {
        // Id de la notificación
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        // Título de la notificación
        [Display(Prompt = "Escribe el título de la notificación", Description = "Título de la notificación", Name = "Título")]
        [Required(ErrorMessage = "Debes indicar un título para la notificación")]
        [StringLength(maximumLength: 200, ErrorMessage = "El título no puede tener más de 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        // Texto del cuerpo de la notificación
        [Display(Prompt = "Escribe el texto del cuerpo de la notificación", Description = "Texto del cuerpo de la notificación", Name = "Texto")]
        [Required(ErrorMessage = "Debes indicar un texto para la notificación")]
        [StringLength(maximumLength: 5000, ErrorMessage = "El texto no puede tener más de 5000 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Texto { get; set; } = string.Empty;

        // Concepto de la notificación
        [Display(Prompt = "Selecciona el concepto de la notificación", Description = "Concepto de la notificación", Name = "Concepto")]
        [Required(ErrorMessage = "Debes indicar un concepto para la notificación")]
        public string Concepto { get; set; } = string.Empty;

        // Receptores de la notificación
        [Display(Prompt = "Selecciona los receptores de la notificación", Description = "Receptores de la notificación", Name = "Receptores")]
        [Required(ErrorMessage = "Debes indicar al menos un receptor para la notificación")]
        public string Receptores { get; set; } = string.Empty;

        // Fecha de publicación de la notificación
        [Display(Prompt="Indica la fecha de publicación de la notificación", Description="Fecha de publicación de la notificación", Name = "Fecha de Publicación")]
        [Required(ErrorMessage = "Debes indicar la fecha de publicación")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }


    }
}
