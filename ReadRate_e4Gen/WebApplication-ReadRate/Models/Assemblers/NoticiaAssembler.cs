using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class NoticiaAssembler
    {
        public NoticiaViewModel ConvertirENToViewModel(NoticiaEN noticiaEN)
        {
            // VALIDACIÓN: Si noticiaEN es null, retornar null o lanzar excepción
            if (noticiaEN == null)
            {
                return null; // O throw new ArgumentNullException(nameof(noticiaEN));
            }

            NoticiaViewModel noticiaVM = new NoticiaViewModel();
            noticiaVM.Id = noticiaEN.Id;
            noticiaVM.Titulo = noticiaEN.Titulo;
            noticiaVM.FechaPublicacion = noticiaEN.FechaPublicacion.HasValue ? noticiaEN.FechaPublicacion.Value : DateTime.MinValue;
            noticiaVM.TextoContenido = noticiaEN.TextoContenido;
            noticiaVM.Foto = noticiaEN.Foto;
            noticiaVM.AdminPublicadorID = noticiaEN.AdministradorNoticias != null ? noticiaEN.AdministradorNoticias.Id : (int?)null;

            // Recuperar el nombre del administrador si está disponible
            noticiaVM.AdminNombre = noticiaEN.AdministradorNoticias?.Nombre;

            return noticiaVM;
        }

        public IList<NoticiaViewModel> ConvertirListENToViewModel(IList<NoticiaEN> noticiaENList)
        {
            IList<NoticiaViewModel> noticiaVMList = new List<NoticiaViewModel>();
            
            if (noticiaENList == null)
                return noticiaVMList;

            foreach (NoticiaEN noticiaEN in noticiaENList)
            {
                // Saltar elementos nulos en la lista
                if (noticiaEN != null)
                {
                    noticiaVMList.Add(ConvertirENToViewModel(noticiaEN));
                }
            }
            return noticiaVMList;
        }
    }
}
