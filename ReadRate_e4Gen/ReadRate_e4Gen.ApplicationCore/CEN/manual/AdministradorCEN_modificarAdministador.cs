
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Administrador_modificarAdministador) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class AdministradorCEN
{
public void ModificarAdministador (int p_Administrador_OID, string p_nombre, String p_pass, string p_email, string p_foto)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Administrador_modificarAdministador_customized) START*/

        AdministradorEN administradorEN = null;

        //Initialized AdministradorEN
        administradorEN = new AdministradorEN ();
        administradorEN.Id = p_Administrador_OID;
        administradorEN.Nombre = p_nombre;
        administradorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        administradorEN.Email = p_email;
        administradorEN.Foto = p_foto;
        //Call to AdministradorRepository

        _IAdministradorRepository.ModificarAdministador (administradorEN);

        /*PROTECTED REGION END*/
}
}
}
