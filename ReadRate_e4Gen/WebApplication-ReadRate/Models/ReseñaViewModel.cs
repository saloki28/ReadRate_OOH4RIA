using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class ReseñaViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //id del libro
        [Display(Name = "Libro reseñado")]
        [Required(ErrorMessage = "Debes seleccionar un libro")]
        public int LibroId { get; set; }

        //Nombre del libro
        [Display(Name = "Nombre del libro reseñado")]
        public string LibroNombre{ get; set; }

        //Valoración del libro
        [Display(Prompt = "Valoración del libro", Description = "Valoración del libro", Name = "Valoración del lector")]
        [Required(ErrorMessage = "Debes incluir un número")]
        [Range(minimum: 0.0, maximum: 5.0, ErrorMessage = "El valor debe estar entre 0 y 5.")] //Ponemos que el rango para la reseña sea de 0 a 5
        public float Valoracion { get; set; }

        //Opinión
        [Display(Prompt = "Opinión personal", Description = "Opinión personal sobre el libro", Name = "Opinión del lector")]
        [Required(ErrorMessage = "Debes incluir una opinión")]
        [StringLength(maximumLength: 600, ErrorMessage = "La reseña no puede tener más de 600 caracteres")]
        public string Opinion { get; set; }

        //Lector que ha realizado la reseña
        [Display(Name = "Lector reseñador")]
        [ScaffoldColumn(false)]
        public int LectorId { get; set; }

        //Nombre del lector de la reseña
        [Display(Name = "Nombre del lector reseñador")]
        public string LectorNombre { get; set; } 

        // Cuando se realizó la reseña
        [Display(Name = "Fecha de Publicación")]
        [DataType(DataType.Date)]
        public DateTime? FechaPublicacion { get; set; }
    }
}
