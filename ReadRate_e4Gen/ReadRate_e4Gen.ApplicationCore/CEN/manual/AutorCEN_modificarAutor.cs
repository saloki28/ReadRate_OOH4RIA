
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Autor_modificarAutor) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class AutorCEN
{
public void ModificarAutor (int p_Autor_OID, string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass, int p_numeroSeguidores, int p_cantidadLibrosPublicados, float p_valoracionMedia)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Autor_modificarAutor_customized) ENABLED START*/

        AutorEN autorEN = null;

        //Initialized AutorEN
        autorEN = new AutorEN ();
        autorEN.Id = p_Autor_OID;
        autorEN.Email = p_email;
        autorEN.NombreUsuario = p_nombreUsuario;
        autorEN.FechaNacimiento = p_fechaNacimiento;
        autorEN.CiudadResidencia = p_ciudadResidencia;
        autorEN.PaisResidencia = p_paisResidencia;
        autorEN.Foto = p_foto;
        autorEN.Rol = p_rol;

        if (!string.IsNullOrWhiteSpace(p_pass) && p_pass.Length != 32)
        {
            // Nueva contraseña: aplicar hash MD5
            autorEN.Pass = Utils.Util.GetEncondeMD5(p_pass);
        }
        else
        {
            // Contraseña actual (ya hasheada) o vacía: mantener tal cual
            autorEN.Pass = p_pass;
        }

        autorEN.NumeroSeguidores = p_numeroSeguidores;
        autorEN.CantidadLibrosPublicados = p_cantidadLibrosPublicados;
        autorEN.ValoracionMedia = p_valoracionMedia;
        //Call to AutorRepository

        _IAutorRepository.ModificarAutor (autorEN);

        /*PROTECTED REGION END*/
}
}
}
