
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Club_obtenerListaMiembros) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class ClubCP : GenericBasicCP
{
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> ObtenerListaMiembros (int p_oid)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Club_obtenerListaMiembros) ENABLED START*/

        ClubCEN clubCEN = null;

        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>  result = null;


        try
        {
                CPSession.SessionInitializeTransaction ();

                clubCEN = new  ClubCEN (CPSession.UnitRepo.ClubRepository);
                ClubEN clubEN = clubCEN.DameClubPorOID (p_oid);

                if (clubEN == null) {
                        throw new ModelException ("El club con ID " + p_oid + " no existe");
                }

                foreach (var item in clubEN.LectorMiembro) {
                        Console.WriteLine (item);
                }

                return clubEN.LectorMiembro;



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
