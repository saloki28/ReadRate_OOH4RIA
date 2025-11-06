
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Notificacion_crearNotificacion) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class NotificacionCEN
{
public int CrearNotificacion (Nullable<DateTime> p_fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum p_concepto, int p_noticiaNotificada, int p_eventoNotificado, int p_clubNotificado, int p_autorAvisado, int p_reseñaNotificada)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Notificacion_crearNotificacion_customized) ENABLED START*/

        NotificacionEN notificacionEN = null;

        int oid;

        //Initialized NotificacionEN
        notificacionEN = new NotificacionEN ();
        notificacionEN.Fecha = DateTime.Now;

        notificacionEN.Concepto = p_concepto;


        if (p_noticiaNotificada != -1) {
                notificacionEN.NoticiaNotificada = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN ();
                notificacionEN.NoticiaNotificada.Id = p_noticiaNotificada;
        }


        if (p_eventoNotificado != -1) {
                notificacionEN.EventoNotificado = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN ();
                notificacionEN.EventoNotificado.Id = p_eventoNotificado;
        }


        if (p_clubNotificado != -1) {
                notificacionEN.ClubNotificado = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN ();
                notificacionEN.ClubNotificado.Id = p_clubNotificado;
        }


        if (p_autorAvisado != -1) {
                notificacionEN.AutorAvisado = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN ();
                notificacionEN.AutorAvisado.Id = p_autorAvisado;
        }


        if (p_reseñaNotificada != -1) {
                notificacionEN.ReseñaNotificada = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN ();
                notificacionEN.ReseñaNotificada.Id = p_reseñaNotificada;
        }

        //Call to NotificacionRepository

        oid = _INotificacionRepository.CrearNotificacion (notificacionEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}
