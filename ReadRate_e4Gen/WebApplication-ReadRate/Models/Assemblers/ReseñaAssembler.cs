using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class ReseñaAssembler
    {
        public ReseñaViewModel ConvertirENToViewModel(ReseñaEN en)
        {
            ReseñaViewModel res = new ReseñaViewModel();
            res.Id = en.Id;
            res.LibroId = en.LibroReseñado?.Id ?? 0;
            res.Valoracion = en.Valoracion;
            res.Opinion = en.TextoOpinion;
            res.LectorId = en.LectorValorador?.Id ?? 0;
            res.FechaPublicacion = en.Fecha;
            res.LibroNombre = en.LibroReseñado?.Titulo ?? "Sin título";
            res.LectorNombre = en.LectorValorador?.NombreUsuario ?? "Sin nombre";

            return res;
        }

        public IList<ReseñaViewModel> ConvertirListENToViewModel(IList<ReseñaEN> ens)
        {
            IList<ReseñaViewModel> ress = new List<ReseñaViewModel>();
            foreach(ReseñaEN en in ens)
            {
                ress.Add(ConvertirENToViewModel(en));
            }
            return ress;
        }

    }
}
