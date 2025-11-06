
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_desuscribirDeClub) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class UsuarioCP : GenericBasicCP
{
public void DesuscribirDeClub (int p_Usuario_OID, System.Collections.Generic.IList<int> p_clubSuscrito_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_desuscribirDeClub) ENABLED START*/

        UsuarioCEN usuarioCEN = null;
        LectorCEN lectorCEN = null;


        ClubCEN clubCEN = null;
        ClubEN clubEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioCEN = new UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);
                lectorCEN = new LectorCEN (CPSession.UnitRepo.LectorRepository);

                UsuarioEN usuarioEN = usuarioCEN.DameUsuarioPorOID (p_Usuario_OID);
                LectorEN lectorEN = lectorCEN.DameLectorPorOID (p_Usuario_OID);
                var existe = false;

                foreach (int clubId in p_clubSuscrito_OIDs) {
                        existe = false;
                        foreach (ClubEN clubUsuarioEN in usuarioEN.ClubSuscrito) {
                                if (clubUsuarioEN.Id == clubId) {
                                        existe = true;
                                        clubUsuarioEN.MiembrosActuales -= 1; //disminuir miembros actuales
                                        clubCEN.get_IClubRepository ().ModificarClub (clubUsuarioEN); //guardar cambios
                                        lectorEN.CantClubsSuscritos -= 1; //disminuir contador de clubs suscritos
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
