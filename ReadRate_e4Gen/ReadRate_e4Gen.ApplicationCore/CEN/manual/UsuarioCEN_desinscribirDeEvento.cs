
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_desinscribirDeEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class UsuarioCEN
{
public void DesinscribirDeEvento (int p_Usuario_OID, System.Collections.Generic.IList<int> p_evento_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_desinscribirDeEvento_customized) START*/


        //Call to UsuarioRepository

        _IUsuarioRepository.DesinscribirDeEvento (p_Usuario_OID, p_evento_OIDs);

        /*PROTECTED REGION END*/
}
}
}
