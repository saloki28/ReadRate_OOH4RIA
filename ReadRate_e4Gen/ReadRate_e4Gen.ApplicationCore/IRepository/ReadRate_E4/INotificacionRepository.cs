
using System;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;

namespace ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4
{
public partial interface INotificacionRepository
{
void setSessionCP (GenericSessionCP session);

NotificacionEN ReadOIDDefault (int id
                               );

void ModifyDefault (NotificacionEN notificacion);

System.Collections.Generic.IList<NotificacionEN> ReadAllDefault (int first, int size);



int CrearNotificacion (NotificacionEN notificacion);

void ModificarNotificacion (NotificacionEN notificacion);


void EliminarNotificacion (int id
                           );


NotificacionEN DameNotificacionPorOID (int id
                                       );


System.Collections.Generic.IList<NotificacionEN> DameTodosNotificaciones (int first, int size);


void VincularNotificacionAutor (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_autorNotificado_OIDs);

void DesvincularNotificacionAutor (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_autorNotificado_OIDs);

void VincularNotificacionLector (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_lectorNotificado_OIDs);

void DesvincularNotificacionLector (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_lectorNotificado_OIDs);
}
}
