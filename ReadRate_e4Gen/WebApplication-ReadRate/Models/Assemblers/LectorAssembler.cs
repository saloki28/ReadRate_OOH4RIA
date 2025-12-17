using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class LectorAssembler
    {
        public LectorViewModel ConvertirENToViewModel(LectorEN en)
        {
            LectorViewModel lec = new LectorViewModel();
            lec.IdUsuario = en.Id;
            lec.Email = en.Email;
            lec.NombreUsuario = en.NombreUsuario;
            lec.FechaNacimiento = en.FechaNacimiento.HasValue ? en.FechaNacimiento.Value : DateTime.MinValue;
            lec.CiudadResidencia = en.CiudadResidencia;
            lec.PaisResidencia = en.PaisResidencia;
            lec.FotoUrl = en.Foto;
            lec.Rol = en.Rol.ToString();
            lec.Pass = en.Pass;
            lec.CantLibrosCurso = en.CantLibrosCurso;
            lec.CantLibrosLeidos = en.CantLibrosLeidos;
            lec.CantClubsSuscritos = en.CantClubsSuscritos;
            lec.CantAutoresSeguidos = en.CantAutoresSeguidos;

            LibroAssembler libroAssembler = new LibroAssembler();
            lec.LibrosGuardados = libroAssembler.ConvertirListENToViewModel(en.LibroLeido ?? new List<LibroEN>());
            lec.LecturasGuardadas = libroAssembler.ConvertirListENToViewModel(en.LibroEnCurso ?? new List<LibroEN>());

            // Combinar clubes suscritos y clubes creados
            ClubAssembler clubAssembler = new ClubAssembler();
            var clubsSuscritos = en.ClubSuscritoLector ?? new List<ClubEN>();
            var clubsCreados = en.ClubCreado ?? new List<ClubEN>();
            var todosLosClubes = clubsSuscritos.Union(clubsCreados).ToList();
            lec.ClubsInscritos = clubAssembler.ConvertirListENToViewModel(todosLosClubes);
            
            return lec;
        }   
        public IList<LectorViewModel> ConvertirListENToViewModel(IList<LectorEN> enList)
        {
            IList<LectorViewModel> lecList = new List<LectorViewModel>();
            foreach (LectorEN en in enList)
            {
                lecList.Add(ConvertirENToViewModel(en));
            }
            return lecList;
        }
    }
}
