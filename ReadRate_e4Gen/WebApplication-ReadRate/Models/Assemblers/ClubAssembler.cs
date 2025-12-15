using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class ClubAssembler
    {
        public ClubViewModel ConvertirENToViewModel(ClubEN en)
        {
            ClubViewModel club = new ClubViewModel();
            club.Id = en.Id;
            club.Nombre = en.Nombre;
            club.Enlace = en.EnlaceDiscord;
            club.NumeroMax = en.MiembrosMax;
            club.Foto = en.Foto;
            club.FotoUrl = en.Foto; // FotoUrl para mostrar en las vistas
            club.Descripcion = en.Descripcion;
            club.Miembros = en.MiembrosActuales;
            club.PropietarioId = en.LectorPropietario?.Id ?? 0;

            return club;
        }

        public IList<ClubViewModel> ConvertirListENToViewModel(IList<ClubEN> ens)
        {
            IList<ClubViewModel> ress = new List<ClubViewModel>();
            foreach (ClubEN en in ens)
            {
                ress.Add(ConvertirENToViewModel(en));
            }
            return ress;
        }

    }
}
