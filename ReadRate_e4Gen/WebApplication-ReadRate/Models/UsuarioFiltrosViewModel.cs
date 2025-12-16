using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class UsuarioFiltrosViewModel
    {
        // Nombre del usuario (input de texto)
        [Display(Prompt = "Buscar por nombre", Description = "Nombre del usuario", Name = "Nombre")]
        public string? NombreFiltro { get; set; }

        // Rol del usuario (Select)
        [Display(Prompt = "Selecciona el rol", Description = "Rol del usuario", Name = "Rol")]
        public string? RolFiltro { get; set; }

        // Lista de usuarios resultantes
        public IEnumerable<UsuarioViewModel> Usuarios { get; set; } = new List<UsuarioViewModel>();

        // Lista de roles para el dropdown
        public IEnumerable<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
    }
}