
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_desuscribirLectorDeClub) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void DesuscribirLectorDeClub (int p_Lector_OID, System.Collections.Generic.IList<int> p_clubSuscritoLector_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_desuscribirLectorDeClub) ENABLED START*/

        LectorCEN lectorCEN = null;
        ClubCEN clubCEN = null;
        ClubEN clubEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new LectorCEN (CPSession.UnitRepo.LectorRepository);
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);

                LectorEN lectorEN = lectorCEN.DameLectorPorOID (p_Lector_OID);
                var existe = false;

                foreach (int clubId in p_clubSuscritoLector_OIDs) {
                        existe = false;
                        foreach (ClubEN clubLectorEN in lectorEN.ClubSuscritoLector) {
                                if (clubLectorEN.Id == clubId) {
                                        existe = true;
                                        clubLectorEN.MiembrosActuales -= 1; // Disminuir miembros actuales en Club
                                        clubCEN.get_IClubRepository ().ModificarClub (clubLectorEN); // Guardar cambios
                                        lectorEN.CantClubsSuscritos -= 1; // Disminuir contador de clubs suscritos en lector
                                        break;
                                }
                        }

                        if (!existe) {
                                throw new Exception ("El usuario no esta suscrito a ese club");
                        }
                }

                lectorCEN.get_ILectorRepository ().ModificarLector (lectorEN);


                // usuarioCEN.get_IUsuarioRepository ().DesuscribirDeClub (p_Usuario_OID, p_clubSuscrito_OIDs);


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
