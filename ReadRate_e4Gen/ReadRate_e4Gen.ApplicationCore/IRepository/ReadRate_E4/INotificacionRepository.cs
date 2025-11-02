
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


void VincularNotificacionAUsuario (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_usuarioNotificado_OIDs);

void DesvincularNotificacionAUsuario (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_usuarioNotificado_OIDs);
}
}
