
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_dejarDeSeguirAutor) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void DejarDeSeguirAutor (int p_Lector_OID, System.Collections.Generic.IList<int> p_autorSeguido_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_dejarDeSeguirAutor) ENABLED START*/

        LectorCEN lectorCEN = null;



        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new  LectorCEN (CPSession.UnitRepo.LectorRepository);

                // Corregido: Usar el repositorio directamente desde LectorCEN
                lectorCEN.get_ILectorRepository ().DejarDeSeguirAutor (p_Lector_OID, p_autorSeguido_OIDs);



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
