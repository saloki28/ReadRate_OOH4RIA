using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;

namespace WebApplication_ReadRate.Models.Assemblers
{
    public class EventoAssembler
    {
        public EventoViewModel ConvertirENToViewModel(EventoEN eventoEN)
        {
            // VALIDACIÓN: Si eventoEN es null, retornar null
            if (eventoEN == null)
            {
                return null;
            }

            EventoViewModel eventoVM = new EventoViewModel();
            eventoVM.Id = eventoEN.Id;
            eventoVM.Nombre = eventoEN.Nombre;
            eventoVM.Foto = eventoEN.Foto;
            eventoVM.TextoDescripcion = eventoEN.Descripcion;
            eventoVM.FechaPublicacion = eventoEN.Fecha.HasValue ? eventoEN.Fecha.Value : DateTime.MinValue;
            eventoVM.HoraEvento = eventoEN.Hora.HasValue ? eventoEN.Hora.Value : DateTime.MinValue;
            eventoVM.Ubicacion = eventoEN.Ubicacion;
            eventoVM.AforoMaximo = eventoEN.AforoMax;
            eventoVM.AforoActual = eventoEN.AforoActual;

            eventoVM.AdminPublicadorID = eventoEN.AdministradorEventos != null ? eventoEN.AdministradorEventos.Id : (int?)null;

            // Recuperar el nombre del administrador si está disponible
            eventoVM.AdminNombre = eventoEN.AdministradorEventos?.Nombre;

            return eventoVM;
        }

        public IList<EventoViewModel> ConvertirListENToViewModel(IList<EventoEN> eventoENList)
        {
            IList<EventoViewModel> eventoVMList = new List<EventoViewModel>();
            
            if (eventoENList == null)
                return eventoVMList;

            foreach (EventoEN eventoEN in eventoENList)
            {
                // Saltar elementos nulos en la lista
                if (eventoEN != null)
                {
                    var eventoVM = ConvertirENToViewModel(eventoEN);
                    if (eventoVM != null)
                    {
                        eventoVMList.Add(eventoVM);
                    }
                }
            }
            return eventoVMList;
        }
    }
}
