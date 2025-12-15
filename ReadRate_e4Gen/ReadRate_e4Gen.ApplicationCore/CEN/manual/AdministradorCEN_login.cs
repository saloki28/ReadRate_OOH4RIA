
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Administrador_login) ENABLED START*/
//  references to other libraries
using Newtonsoft.Json;

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class AdministradorCEN
{
// Agrega este metodo privado en la clase AdministradorCEN para solucionar el error


public string Login (string p_email, string p_pass)
{
        string result = null;

        IList<AdministradorEN> enList = _IAdministradorRepository.DameAdministradoresPorEmail (p_email);

        AdministradorEN en = enList.Count > 0 ? enList [0] : null;

        if (en != null && en.Pass.Equals (Utils.Util.GetEncondeMD5 (p_pass)))
                result = this.GetToken (en.Id);

        return result;
}
}
}

/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class AdministradorCEN
{
}
}
