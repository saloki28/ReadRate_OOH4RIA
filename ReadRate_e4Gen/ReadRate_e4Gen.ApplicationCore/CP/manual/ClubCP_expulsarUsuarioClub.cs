
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
        LectorCEN lectorCEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);
                lectorCEN = new LectorCEN (CPSession.UnitRepo.LectorRepository);

                LectorEN lector = lectorCEN.DameLectorPorOID (p_usuario_OID); // Obtener el usuario a expulsar

                // Obtener el club directamente del repositorio para mantener la sesión activa
                ClubEN club = CPSession.UnitRepo.ClubRepository.DameClubPorOID (p_oid);

                if (club == null) { // Verificar si el club existe
                        throw new ModelException ("El club no existe.");
                }

                // Cargar la colección de miembros forzando su acceso mientras la sesión está activa
                var encontrado = false;
                int cantidadMiembros = 0;

                if (club.LectorMiembro != null) {
                        foreach (LectorEN l in club.LectorMiembro) {
                                cantidadMiembros++;
                                if (l.Id == p_usuario_OID) {
                                        encontrado = true;
                                }
                        }
                }

                if (cantidadMiembros == 0) {
                        throw new ModelException ("El club no tiene miembros.");
                }

                if (!encontrado) { // Verificar si el usuario es miembro del club
                        throw new ModelException ("El usuario no es miembro de este club.");
                }

                // Disminuir el contador de miembros actuales del club
                club.MiembrosActuales -= 1;

                lectorCEN.get_ILectorRepository ().DesuscribirLectorDeClub (p_usuario_OID, new List<int>() {
                                p_oid
                        });

                CPSession.Commit ();
        }
        catch (Exception)
        {
                CPSession.RollBack ();
                throw;
        }
        finally
        {
                CPSession.SessionClose ();
        }


        /*PROTECTED REGION END*/
}
}
}
