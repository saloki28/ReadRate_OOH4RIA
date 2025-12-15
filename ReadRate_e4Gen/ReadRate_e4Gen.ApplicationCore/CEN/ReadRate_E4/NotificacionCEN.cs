

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

public void ModificarNotificacion (int p_Notificacion_OID, Nullable<DateTime> p_fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum p_concepto, string p_tituloResumen, string p_textoCuerpo)
{
        NotificacionEN notificacionEN = null;

        //Initialized NotificacionEN
        notificacionEN = new NotificacionEN ();
        notificacionEN.Id = p_Notificacion_OID;
        notificacionEN.Fecha = p_fecha;
        notificacionEN.Concepto = p_concepto;
        notificacionEN.TituloResumen = p_tituloResumen;
        notificacionEN.TextoCuerpo = p_textoCuerpo;
        //Call to NotificacionRepository

        _INotificacionRepository.ModificarNotificacion (notificacionEN);
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
public void VincularNotificacionAutor (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_autorNotificado_OIDs)
{
        //Call to NotificacionRepository

        _INotificacionRepository.VincularNotificacionAutor (p_Notificacion_OID, p_autorNotificado_OIDs);
}
public void DesvincularNotificacionAutor (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_autorNotificado_OIDs)
{
        //Call to NotificacionRepository

        _INotificacionRepository.DesvincularNotificacionAutor (p_Notificacion_OID, p_autorNotificado_OIDs);
}
public void VincularNotificacionLector (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_lectorNotificado_OIDs)
{
        //Call to NotificacionRepository

        _INotificacionRepository.VincularNotificacionLector (p_Notificacion_OID, p_lectorNotificado_OIDs);
}
public void DesvincularNotificacionLector (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_lectorNotificado_OIDs)
{
        //Call to NotificacionRepository

        _INotificacionRepository.DesvincularNotificacionLector (p_Notificacion_OID, p_lectorNotificado_OIDs);
}
}
}
