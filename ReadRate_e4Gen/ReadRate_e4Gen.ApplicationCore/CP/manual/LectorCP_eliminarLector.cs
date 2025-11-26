
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_eliminarLector) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void EliminarLector (int p_Lector_OID)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_eliminarLector) ENABLED START*/

        LectorCEN lectorCEN = null;
        EventoCEN eventoCEN = null;
        ClubCEN clubCEN = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new  LectorCEN (CPSession.UnitRepo.LectorRepository);
                LectorEN lector = lectorCEN.DameLectorPorOID (p_Lector_OID); // Obtener el lector a eliminar


                if (lector.ClubCreado != null && lector.ClubCreado.Count > 0) { // Verificar si el lector tiene clubes creados
                        // Impedir que se elimine el lector si es propietario de clubes
                        throw new ModelException ("---------------- No se puede eliminar el lector porque es propietario de uno o más clubes.--------------------");
                }

                //-------------------------------------------------------------------
                List<EventoEN> eventosInscritos = new List<EventoEN>(); // Lista para almacenar los eventos en los que el usuario está inscrito

                if (lector.EventoLector != null) { // Verificar si el usuario tiene eventos inscritos
                        eventosInscritos = lector.EventoLector as List<EventoEN>;

                        // Eliminar al usuario de los eventos en los que está inscrito
                        foreach (EventoEN evento in eventosInscritos) {
                                lectorCEN.get_ILectorRepository ().DesinscribirLectorDeEvento (p_Lector_OID, new List<int>() {
                                                evento.Id
                                        });
                        }
                }

                //-------------------------------------------------------------------
                List<ClubEN> clubesSuscritos = new List<ClubEN>(); // Lista para almacenar los clubes a los que el usuario está suscrito

                if (lector.ClubSuscritoLector != null) { // Verificar si el usuario tiene clubes suscritos
                        clubesSuscritos = lector.ClubSuscritoLector as List<ClubEN>;

                        // Eliminar al usuario de los clubes a los que está suscrito
                        foreach (ClubEN club in clubesSuscritos) {
                                lectorCEN.get_ILectorRepository ().DesuscribirLectorDeClub (p_Lector_OID, new List<int>() {
                                                club.Id
                                        });
                        }
                }

                lectorCEN.get_ILectorRepository ().EliminarLector (p_Lector_OID); // Eliminar el lector

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


        /*PROTECTED REGION END*/
}
}
}
