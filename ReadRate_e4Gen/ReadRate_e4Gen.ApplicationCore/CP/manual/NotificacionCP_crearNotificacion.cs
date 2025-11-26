
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Notificacion_crearNotificacion) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class NotificacionCP : GenericBasicCP
{
public ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN CrearNotificacion (Nullable<DateTime> p_fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum p_concepto, int p_OID_destino, string p_tituloResumen, string p_textoCuerpo)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Notificacion_crearNotificacion) ENABLED START*/

        NotificacionCEN notificacionCEN = null;
        NoticiaCEN noticiaCEN = null;
        ReseñaCEN reseñaCEN = null;
        EventoCEN eventoCEN = null;
        ClubCEN clubCEN = null;

        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN result = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                notificacionCEN = new  NotificacionCEN (CPSession.UnitRepo.NotificacionRepository);
                noticiaCEN = new NoticiaCEN (CPSession.UnitRepo.NoticiaRepository);
                reseñaCEN = new ReseñaCEN (CPSession.UnitRepo.ReseñaRepository);
                eventoCEN = new EventoCEN (CPSession.UnitRepo.EventoRepository);
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);

                NotificacionEN notificacionEN = null;

                //Initialized NotificacionEN
                notificacionEN = new NotificacionEN();
                notificacionEN.Fecha = p_fecha ?? DateTime.Now;
                notificacionEN.Concepto = p_concepto;
                notificacionEN.TituloResumen = p_tituloResumen;
                notificacionEN.TextoCuerpo = p_textoCuerpo;

                switch (p_concepto) {
                case ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum.noticia_publicada:
                        NoticiaEN noticiaEN = noticiaCEN.DameNoticiaPorOID (p_OID_destino);
                        notificacionEN.NoticiaNotificada = noticiaEN;
                        break;

                case ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum.evento_anunciado:
                        EventoEN eventoEN = eventoCEN.DameEventoPorOID (p_OID_destino);
                        notificacionEN.EventoNotificado = eventoEN;
                        break;

                case ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum.aviso_club_lectura:
                        ClubEN clubEN = clubCEN.DameClubPorOID (p_OID_destino);
                        notificacionEN.ClubNotificado = clubEN;
                        break;

                case ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum.nueva_reseña:
                        ReseñaEN reseñaEN = reseñaCEN.DameReseñaPorOID (p_OID_destino);
                        notificacionEN.ReseñaNotificada = reseñaEN;
                        break;
                }

                int id = notificacionCEN.get_INotificacionRepository ().CrearNotificacion (notificacionEN);

                result = notificacionCEN.get_INotificacionRepository ().DameNotificacionPorOID (id);

                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw ex;
        }
        finally
        {
                CPSession.SessionClose ();
        }
        return result;


        /*PROTECTED REGION END*/
}
}
}
