
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_asignarLibroListaGuardados) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void AsignarLibroListaGuardados (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroLeido_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_asignarLibroListaGuardados) ENABLED START*/

        LectorCEN lectorCEN = null;



        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new  LectorCEN (CPSession.UnitRepo.LectorRepository);







                lectorCEN.get_ILectorRepository ().AsignarLibroListaGuardados (p_Lector_OID, p_libroLeido_OIDs);



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
