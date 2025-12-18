using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Lector_modificarLector) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
    public partial class LectorCEN
    {
        public void ModificarLector (int p_Lector_OID, string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass, int p_cantLibrosCurso, int p_cantLibrosLeidos, int p_cantAutoresSeguidos, int p_cantClubsSuscritos)
        {
            /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Lector_modificarLector_customized) ENABLED START*/

            LectorEN lectorEN = null;

            //Initialized LectorEN
            lectorEN = new LectorEN ();
            lectorEN.Id = p_Lector_OID;
            lectorEN.Email = p_email;
            lectorEN.NombreUsuario = p_nombreUsuario;
            lectorEN.FechaNacimiento = p_fechaNacimiento;
            lectorEN.CiudadResidencia = p_ciudadResidencia;
            lectorEN.PaisResidencia = p_paisResidencia;
            lectorEN.Foto = p_foto;
            lectorEN.Rol = p_rol;

            // Solo hash si no lo está aun
            if (!string.IsNullOrWhiteSpace(p_pass) && p_pass.Length != 32)
            {
                // Nueva contraseña: aplicar hash MD5
                lectorEN.Pass = Utils.Util.GetEncondeMD5(p_pass);
            }
            else
            {
                // Contraseña actual (ya hasheada) o vacía: mantener tal cual
                lectorEN.Pass = p_pass;
            }

            lectorEN.CantLibrosCurso = p_cantLibrosCurso;
            lectorEN.CantLibrosLeidos = p_cantLibrosLeidos;
            lectorEN.CantAutoresSeguidos = p_cantAutoresSeguidos;
            lectorEN.CantClubsSuscritos = p_cantClubsSuscritos;
            
            //Call to LectorRepository
            
            _ILectorRepository.ModificarLector (lectorEN);

            /*PROTECTED REGION END*/
        }
    }
}
