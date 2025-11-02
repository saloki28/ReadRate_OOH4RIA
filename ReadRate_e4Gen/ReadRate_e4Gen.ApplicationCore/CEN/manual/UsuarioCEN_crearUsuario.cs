
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_crearUsuario) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class UsuarioCEN
{
private int CrearUsuario (string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Usuario_crearUsuario_customized) START*/

        UsuarioEN usuarioEN = null;

        int oid;

        //Initialized UsuarioEN
        usuarioEN = new UsuarioEN ();
        usuarioEN.Email = p_email;

        usuarioEN.NombreUsuario = p_nombreUsuario;

        usuarioEN.FechaNacimiento = p_fechaNacimiento;

        usuarioEN.CiudadResidencia = p_ciudadResidencia;

        usuarioEN.PaisResidencia = p_paisResidencia;

        usuarioEN.Foto = p_foto;

        usuarioEN.Rol = p_rol;

        usuarioEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);

        //Call to UsuarioRepository

        oid = _IUsuarioRepository.CrearUsuario (usuarioEN);
        return oid;
        /*PROTECTED REGION END*/
}
}
}
