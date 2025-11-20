
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Club_expulsarUsuarioClub) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class ClubCP : GenericBasicCP
{
public void ExpulsarUsuarioClub (int p_oid, int p_usuario_OID)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Club_expulsarUsuarioClub) ENABLED START*/

        ClubCEN clubCEN = null;
        UsuarioCEN usuarioCEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);
                usuarioCEN = new UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);

                UsuarioEN usuario = usuarioCEN.DameUsuarioPorOID (p_usuario_OID); // Obtener el usuario a expulsar

                ClubEN club = clubCEN.DameClubPorOID (p_oid); // Obtener el club del que se expulsa al usuario

                if (club == null) { // Verificar si el club existe
                        throw new ModelException ("El club no existe.");
                }

                List<UsuarioEN> usuarios = club.UsuarioMiembro as List<UsuarioEN>;
                var encontrado = false;

                if (usuarios == null) {
                        throw new ModelException ("El club no tiene miembros.");
                }

                foreach (UsuarioEN u in usuarios) {
                        if (u.Id == p_usuario_OID) {
                                encontrado = true;
                                break;
                        }
                }

                if (!encontrado) { // Verificar si el usuario es miembro del club
                        throw new ModelException ("El usuario no es miembro de este club.");
                }

                // disminuir el contador de miembros actuales del club
                club.MiembrosActuales -= 1;

                // Esta prueba no funciona porque DesuscribirDeClub() es el m�todo que no funciona (fallo de herencia!!!!)
                // usuarioCEN.get_IUsuarioRepository ().DesuscribirDeClub (p_usuario_OID, new List<int>() {p_oid});

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
