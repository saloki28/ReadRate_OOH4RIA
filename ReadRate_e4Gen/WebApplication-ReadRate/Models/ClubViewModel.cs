using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_ReadRate.Models
{
    public class ClubViewModel
    {
        //Id del club
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //Nombre del club
        [Display(Prompt = "Nombre club", Description = "Nombre del club", Name = "Nombre del Club")]
        [Required(ErrorMessage = "Debes incluir un nombre")]
        [StringLength(maximumLength: 200, ErrorMessage = "El nombre no puede tener más de 200 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        //Enlace discord
        [Display(Prompt = "Enlace discord", Description = "Enlace de discord para acceder al club", Name = "Enlace a Discord")]
        [Required(ErrorMessage = "Debes incluir un enlace a discord")]
        [StringLength(maximumLength: 200, ErrorMessage = "El enlace a discord no puede tener más de 200 caracteres")]
        public string Enlace { get; set; } = string.Empty;

        //Numero máximo del club
        [Display(Prompt = "Aforo del club", Description = "Número máximo de personas en el club", Name = "Número Máximo de Miembros")]
        [Required(ErrorMessage = "Debes incluir un aforo máximo")]
        public int NumeroMax { get; set; }

        //Archivo de foto del club (para upload)
        [Display(Name = "Foto")]
        public IFormFile? FotoFile { get; set; }

        //URL de la foto del club (para display)
        public string? FotoUrl { get; set; }

        //Foto del club (ruta en BD)
        [Display(Prompt = "Foto del club", Description = "Foto principal del club", Name = "Foto del Club")]
        [StringLength(maximumLength: 200, ErrorMessage = "La foto no puede tener más de 200 caracteres")]
        public string Foto { get; set; } = string.Empty;

        //Descripcion
        [Display(Prompt = "Descripcion del club", Description = "Descripción sobre lo que trata el club", Name = "Descripción")]
        [Required(ErrorMessage = "Debes incluir una descripción")]
        [StringLength(maximumLength: 255, ErrorMessage = "La descripción no puede tener más de 255 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        //Miembros actuales
        [Display(Prompt = "Miembros actuales", Description = "Miembros actuales del club", Name = "Número Actual de Miembros")]
        [Required(ErrorMessage = "Debes incluir el número de miembros actuales")]
        public int Miembros { get; set; }

        //Id del propietario para el formulario
        [Display(Prompt = "ID del propietario", Description = "ID del usuario propietario", Name = "Propietario del Club")]
        [Required(ErrorMessage = "Debes incluir el ID del propietario")]
        public int PropietarioId { get; set; }

        //Nombre del propietario (para mostrar en vistas)
        [Display(Name = "Propietario del Club")]
        public string? PropietarioNombre { get; set; }
    }
}
