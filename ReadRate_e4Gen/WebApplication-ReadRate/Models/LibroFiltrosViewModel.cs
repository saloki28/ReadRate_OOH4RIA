using Microsoft.AspNetCore.Mvc.Rendering;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class LibroFiltrosViewModel
    {

        // Todas las propiedades son nullable (?) porque los filtros son opcionales

        // Título del libro (input de texto)
        [Display(Prompt = "Buscar por título", Description = "Título del libro", Name = "Título")]
        public string? TituloFiltro { get; set; }

        // Género del libro (Select)
        [Display(Prompt = "Selecciona el género", Description = "Género del libro", Name = "Género")]
        public string? GeneroFiltro { get; set; }

        // Edad Recomendada del libro (input num)
        [Display(Prompt = "Edad mínima recomendada", Description = "Edad Recomendada del libro", Name = "Edad Recomendada")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "La edad debe estar entre 0 y 100 años")]
        public int? EdadRecomendadaFiltro { get; set; }

        // Valoración media del libro (input num)
        [Display(Prompt = "Valoración mínima", Description = "Valoración media del libro", Name = "Valoración Media")]
        [Range(minimum: 0.0, maximum: 5.0, ErrorMessage = "La valoración debe estar entre 0 y 5")]
        public float? ValoracionMediaFiltro { get; set; }

        // Nombre del autor del libro (input de texto)
        [Display(Prompt = "Buscar por autor", Description = "Nombre del autor del libro", Name = "Autor")]
        public string? NombreAutorFiltro { get; set; }

        // Id del autor del libro (interno)
        [ScaffoldColumn(false)]
        public int? AutorIdFiltrar { get; set; }

        // Lista de libros resultantes
        public IEnumerable<LibroViewModel> Libros { get; set; } = new List<LibroViewModel>();

        // Lista de géneros para el dropdown
        public IEnumerable<SelectListItem> Generos { get; set; } = new List<SelectListItem>();
    }
}
