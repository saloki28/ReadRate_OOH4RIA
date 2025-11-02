
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Notificacion_modificarNotificacion) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class NotificacionCEN
{
public void ModificarNotificacion (int p_Notificacion_OID, Nullable<DateTime> p_fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum p_concepto)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Notificacion_modificarNotificacion_customized) START*/

        NotificacionEN notificacionEN = null;

        //Initialized NotificacionEN
        notificacionEN = new NotificacionEN ();
        notificacionEN.Id = p_Notificacion_OID;
        notificacionEN.Fecha = p_fecha;
        notificacionEN.Concepto = p_concepto;
        //Call to NotificacionRepository

        _INotificacionRepository.ModificarNotificacion (notificacionEN);

        /*PROTECTED REGION END*/
}
}
}
