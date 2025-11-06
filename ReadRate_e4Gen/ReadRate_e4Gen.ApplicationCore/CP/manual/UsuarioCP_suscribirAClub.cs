
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_suscribirAClub) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class UsuarioCP : GenericBasicCP
{
public void SuscribirAClub (int p_Usuario_OID, System.Collections.Generic.IList<int> p_clubSuscrito_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_suscribirAClub) ENABLED START*/

        UsuarioCEN usuarioCEN = null;
        ClubCEN clubCEN = null;
        ClubEN clubEN = null;



        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);
                LectorCEN lectorCEN = new LectorCEN (CPSession.UnitRepo.LectorRepository);
                LectorEN lectorEN = lectorCEN.DameLectorPorOID (p_Usuario_OID);


                //comprobar aforo maximo y aumentar aforo actual
                foreach (int clubId in p_clubSuscrito_OIDs) { //club a suscribir
                        clubEN = clubCEN.DameClubPorOID (clubId);

                        if (clubEN.MiembrosActuales >= clubEN.MiembrosMax) { //aforo maximo alcanzado
                                throw new ModelException ("No se puede suscribir al club " + clubEN.Nombre + " porque ha alcanzado su aforo máximo.");
                        }

                        if (clubEN.UsuarioPropietario.Id == p_Usuario_OID) {
                                throw new ModelException ("No se puede suscribir al club " + clubEN.Nombre + " porque el usuario es el propietario del club.");
                        }

                        if (clubEN.UsuarioMiembro.Contains (usuarioCEN.DameUsuarioPorOID (p_Usuario_OID))) {
                                throw new ModelException ("No se puede suscribir al club " + clubEN.Nombre + " porque el usuario ya es miembro del club.");
                        }

                        clubEN.MiembrosActuales += 1; //aumentar miembros actuales
                        lectorEN.CantClubsSuscritos += 1; //aumentar contador de clubs suscritos
                        lectorCEN.get_ILectorRepository ().ModificarLector (lectorEN);
                        clubCEN.get_IClubRepository ().ModificarClub (clubEN); //guardar cambios
                }

                usuarioCEN.get_IUsuarioRepository ().SuscribirAClub (p_Usuario_OID, p_clubSuscrito_OIDs);

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
