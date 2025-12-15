using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class NotificacionAssembler
    {
        public NotificacionViewModel ConvertirENToViewModel(NotificacionEN notificacionEN)
        {
            NotificacionViewModel notificacionVM = new NotificacionViewModel();
            notificacionVM.Id = notificacionEN.Id;
            notificacionVM.Titulo = notificacionEN.TituloResumen;
            notificacionVM.Texto = notificacionEN.TextoCuerpo;
            notificacionVM.Concepto = notificacionEN.Concepto.ToString();
            notificacionVM.Fecha = notificacionEN.Fecha ?? DateTime.MinValue;;

            // Receptores: join nombres de usuario de lectores o autores notificados
            if (notificacionEN.LectorNotificado != null && notificacionEN.LectorNotificado.Count > 0)
            {
                notificacionVM.Receptores = string.Join(", ", notificacionEN.LectorNotificado.Select(l => l.NombreUsuario));
            }
            else if (notificacionEN.AutorNotificado != null && notificacionEN.AutorNotificado.Count > 0)
            {
                notificacionVM.Receptores = string.Join(", ", notificacionEN.AutorNotificado.Select(a => a.NombreUsuario));
            }
            else
            {
                notificacionVM.Receptores = string.Empty;
            }
            return notificacionVM;
        }

        public IList<NotificacionViewModel> ConvertirListENToViewModel(IList<NotificacionEN> notificacionENList)
        {
            IList<NotificacionViewModel> notificacionVMList = new List<NotificacionViewModel>();
            foreach (NotificacionEN notificacionEN in notificacionENList)
            {
                notificacionVMList.Add(ConvertirENToViewModel(notificacionEN));
            }
            return notificacionVMList;
        }
    }
}
