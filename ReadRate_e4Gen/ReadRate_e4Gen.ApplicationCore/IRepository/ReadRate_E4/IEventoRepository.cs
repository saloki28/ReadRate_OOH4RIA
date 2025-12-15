
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface IEventoRepository
{
void setSessionCP (GenericSessionCP session);

EventoEN ReadOIDDefault (int id
                         );

void ModifyDefault (EventoEN evento);

System.Collections.Generic.IList<EventoEN> ReadAllDefault (int first, int size);



int CrearEvento (EventoEN evento);

void ModificarEvento (EventoEN evento);


void EliminarEvento (int id
                     );


EventoEN DameEventoPorOID (int id
                           );


System.Collections.Generic.IList<EventoEN> DameTodosEventos (int first, int size);


System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> DameEventosPorFecha (Nullable<DateTime> p_fecha, int first, int size);
}
}
