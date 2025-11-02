
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Administrador_crearAdministador) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class AdministradorCEN
{
public int CrearAdministador (string p_nombre, String p_pass)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Administrador_crearAdministador_customized) START*/

        AdministradorEN administradorEN = null;

        int oid;

        //Initialized AdministradorEN
        administradorEN = new AdministradorEN ();
        administradorEN.Nombre = p_nombre;

        administradorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);

        //Call to AdministradorRepository

        oid = _IAdministradorRepository.CrearAdministador (administradorEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}
