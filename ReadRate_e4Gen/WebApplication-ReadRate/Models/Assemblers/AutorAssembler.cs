using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class AutorAssembler
    {
        public AutorViewModel ConvertirENToViewModel(AutorEN en)
        {
            AutorViewModel aut = new AutorViewModel();
            aut.IdUsuario = en.Id;
            aut.Email = en.Email;
            aut.NombreUsuario = en.NombreUsuario;
            aut.FechaNacimiento = en.FechaNacimiento.HasValue ? en.FechaNacimiento.Value : DateTime.MinValue;
            aut.CiudadResidencia = en.CiudadResidencia;
            aut.PaisResidencia = en.PaisResidencia;
            aut.FotoUrl = en.Foto; // URL de la foto guardada en BD
            aut.Rol = en.Rol.ToString();
            aut.ValoracionMedia = en.ValoracionMedia;
            aut.Pass = en.Pass;
            aut.NumeroSeguidores = en.NumeroSeguidores;
            aut.CantidadLibrosPublicados = en.CantidadLibrosPublicados;
            return aut;
        }

        public IList<AutorViewModel> ConvertirListENToViewModel(IList<AutorEN> enList)
        {
            IList<AutorViewModel> autList = new List<AutorViewModel>();
            foreach (AutorEN en in enList)
            {
                autList.Add(ConvertirENToViewModel(en));
            }
            return autList;
        }

    }
}
