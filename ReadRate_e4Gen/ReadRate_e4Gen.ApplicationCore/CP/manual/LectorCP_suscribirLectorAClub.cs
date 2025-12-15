
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_suscribirLectorAClub) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void SuscribirLectorAClub (int p_Lector_OID, System.Collections.Generic.IList<int> p_clubSuscritoLector_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_suscribirLectorAClub) ENABLED START*/

        LectorCEN lectorCEN = null;
        ClubCEN clubCEN = null;
        ClubEN clubEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new LectorCEN (CPSession.UnitRepo.LectorRepository);
                clubCEN = new ClubCEN (CPSession.UnitRepo.ClubRepository);
                LectorEN lectorEN = lectorCEN.DameLectorPorOID (p_Lector_OID);

                // Comprobar aforo maximo y aumentar aforo actual
                foreach (int clubId in p_clubSuscritoLector_OIDs) { //club a suscribir
                        clubEN = clubCEN.DameClubPorOID (clubId);

                        if (clubEN.MiembrosActuales >= clubEN.MiembrosMax) { //aforo maximo alcanzado
                                throw new ModelException ("No se puede suscribir al club " + clubEN.Nombre + " porque ha alcanzado su aforo máximo.");
                        }

                        if (clubEN.LectorPropietario.Id == p_Lector_OID) {
                                throw new ModelException ("No se puede suscribir al club " + clubEN.Nombre + " porque el usuario es el propietario del club.");
                        }

                        if (clubEN.LectorMiembro.Contains (lectorCEN.DameLectorPorOID (p_Lector_OID))) {
                                throw new ModelException ("No se puede suscribir al club " + clubEN.Nombre + " porque el usuario ya es miembro del club.");
                        }

                        clubEN.MiembrosActuales += 1; // Aumentar miembros actuales de Club
                        lectorEN.CantClubsSuscritos += 1; // Aumentar contador de clubs suscritos en Lector
                        lectorCEN.get_ILectorRepository ().ModificarLector (lectorEN);
                        clubCEN.get_IClubRepository ().ModificarClub (clubEN); // Guardar cambios
                }

                lectorCEN.get_ILectorRepository ().SuscribirLectorAClub (p_Lector_OID, p_clubSuscritoLector_OIDs);

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
