
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
public void ExpulsarUsuarioClub (int p_oid)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Club_expulsarUsuarioClub) ENABLED START*/

        ClubCEN clubCEN = null;



        try
        {
                CPSession.SessionInitializeTransaction ();
                clubCEN = new  ClubCEN (CPSession.UnitRepo.ClubRepository);



                // Write here your custom transaction ...

                throw new NotImplementedException ("Method ExpulsarUsuarioClub() not yet implemented.");



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
