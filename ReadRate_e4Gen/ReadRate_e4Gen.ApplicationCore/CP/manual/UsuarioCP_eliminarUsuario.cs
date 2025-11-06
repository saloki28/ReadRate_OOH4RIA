
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_eliminarUsuario) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class UsuarioCP : GenericBasicCP
{
public void EliminarUsuario (int p_Usuario_OID)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_eliminarUsuario) ENABLED START*/

        UsuarioCEN usuarioCEN = null;
        EventoCEN eventoCEN = null;
        ClubCEN clubCEN = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                UsuarioEN usuario = usuarioCEN.DameUsuarioPorOID (p_Usuario_OID); // Obtener el usuario a eliminar


                if (usuario.ClubCreado != null && usuario.ClubCreado.Count > 0) { // Verificar si el usuario tiene clubes creados
                        // Impedir que se elimine el usuario si es propietario de clubes
                        throw new ModelException ("---------------- No se puede eliminar el usuario porque es propietario de uno o más clubes.--------------------");
                }

                //-------------------------------------------------------------------
                List<EventoEN> eventosInscritos = new List<EventoEN>(); // Lista para almacenar los eventos en los que el usuario está inscrito

                if (usuario.Evento != null) { // Verificar si el usuario tiene eventos inscritos
                        eventosInscritos = usuario.Evento as List<EventoEN>;

                        // Eliminar al usuario de los eventos en los que está inscrito
                        foreach (EventoEN evento in eventosInscritos) {
                                usuarioCEN.get_IUsuarioRepository ().DesinscribirDeEvento (p_Usuario_OID, new List<int>() {
                                                evento.Id
                                        });
                        }
                }

                //-------------------------------------------------------------------
                List<ClubEN> clubesSuscritos = new List<ClubEN>(); // Lista para almacenar los clubes a los que el usuario está suscrito

                if (usuario.ClubSuscrito != null) { // Verificar si el usuario tiene clubes suscritos
                        clubesSuscritos = usuario.ClubSuscrito as List<ClubEN>;

                        // Eliminar al usuario de los clubes a los que está suscrito
                        foreach (ClubEN club in clubesSuscritos) {
                                usuarioCEN.get_IUsuarioRepository ().DesuscribirDeClub (p_Usuario_OID, new List<int>() {
                                                club.Id
                                        });
                        }
                }

                usuarioCEN.get_IUsuarioRepository ().EliminarUsuario (p_Usuario_OID); // Eliminar el usuario

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
