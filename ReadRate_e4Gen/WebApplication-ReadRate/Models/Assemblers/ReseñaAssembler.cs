using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class ReseñaAssembler
    {
        public ReseñaViewModel ConvertirENToViewModel(ReseñaEN en)
        {
            ReseñaViewModel res = new ReseñaViewModel();
            res.Id = en.Id;
            res.LibroId = en.LibroReseñado.Id;
            res.Valoracion = en.Valoracion;
            res.Opinion = en.TextoOpinion;
            res.LectorId = en.LectorValorador.Id;
            res.FechaPublicacion = en.Fecha;
            res.LibroNombre = en.LibroReseñado.Titulo;
            res.LectorNombre = en.LectorValorador.NombreUsuario;

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
