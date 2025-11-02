

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class NotificacionCEN
 *
 */
public partial class NotificacionCEN
{
private INotificacionRepository _INotificacionRepository;

public NotificacionCEN(INotificacionRepository _INotificacionRepository)
{
        this._INotificacionRepository = _INotificacionRepository;
}

public INotificacionRepository get_INotificacionRepository ()
{
        return this._INotificacionRepository;
}

public void EliminarNotificacion (int id
                                  )
{
        _INotificacionRepository.EliminarNotificacion (id);
}

public NotificacionEN DameNotificacionPorOID (int id
                                              )
{
        NotificacionEN notificacionEN = null;

        notificacionEN = _INotificacionRepository.DameNotificacionPorOID (id);
        return notificacionEN;
}

public System.Collections.Generic.IList<NotificacionEN> DameTodosNotificaciones (int first, int size)
{
        System.Collections.Generic.IList<NotificacionEN> list = null;

        list = _INotificacionRepository.DameTodosNotificaciones (first, size);
        return list;
}
public void VincularNotificacionAUsuario (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_usuarioNotificado_OIDs)
{
        //Call to NotificacionRepository

        _INotificacionRepository.VincularNotificacionAUsuario (p_Notificacion_OID, p_usuarioNotificado_OIDs);
}
public void DesvincularNotificacionAUsuario (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_usuarioNotificado_OIDs)
{
        //Call to NotificacionRepository

        _INotificacionRepository.DesvincularNotificacionAUsuario (p_Notificacion_OID, p_usuarioNotificado_OIDs);
}
}
}
