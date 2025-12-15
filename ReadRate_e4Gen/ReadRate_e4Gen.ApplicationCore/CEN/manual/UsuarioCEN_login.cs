
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_login) ENABLED START*/
//  references to other libraries

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class UsuarioCEN
{
public string Login (string p_email, string p_pass)
{
        string result = null;

        IList<UsuarioEN> enList = _IUsuarioRepository.DameUsuarioPorEmail (p_email);

        UsuarioEN en = enList.Count > 0 ? enList [0] : null;

        if (en == null) {
                return result;
        }

        string pass1 = p_pass;

        for (int i = 0; i <= en.NumModificaciones; i++) {
                pass1 = Utils.Util.GetEncondeMD5 (pass1);
        }

        if (en != null && en.Pass.Equals (pass1))
                result = this.GetToken (en.Id);

        return result;
}
}
}

/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class UsuarioCEN
{
}
}
