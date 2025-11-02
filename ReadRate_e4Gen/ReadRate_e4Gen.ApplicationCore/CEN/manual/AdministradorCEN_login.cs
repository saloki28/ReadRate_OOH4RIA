
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
// Agrega este m�todo privado en la clase AdministradorCEN para solucionar el error CS0103
private object ObtenerID (string decodedToken)
{
        // Suponiendo que el token decodificado es un JSON con un campo "id"
        // Puedes ajustar la l�gica seg�n el formato real del token
        var payload = JsonConvert.DeserializeObject<Dictionary<string, object> >(decodedToken);

        if (payload != null && payload.ContainsKey ("id")) {
                return payload ["id"];
        }
        throw new ModelException ("El token no contiene el campo 'id'");
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
