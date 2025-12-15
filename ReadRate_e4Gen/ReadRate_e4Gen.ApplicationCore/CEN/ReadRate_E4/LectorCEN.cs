

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class LectorCEN
 *
 */
public partial class LectorCEN
{
private ILectorRepository _ILectorRepository;

public LectorCEN(ILectorRepository _ILectorRepository)
{
        this._ILectorRepository = _ILectorRepository;
}

public ILectorRepository get_ILectorRepository ()
{
        return this._ILectorRepository;
}

public int CrearLector (string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass, int p_numModificaciones, int p_cantLibrosCurso, int p_cantLibrosLeidos, int p_cantAutoresSeguidos, int p_cantClubsSuscritos)
{
        LectorEN lectorEN = null;
        int oid;

        //Initialized LectorEN
        lectorEN = new LectorEN ();
        lectorEN.Email = p_email;

        lectorEN.NombreUsuario = p_nombreUsuario;

        lectorEN.FechaNacimiento = p_fechaNacimiento;

        lectorEN.CiudadResidencia = p_ciudadResidencia;

        lectorEN.PaisResidencia = p_paisResidencia;

        lectorEN.Foto = p_foto;

        lectorEN.Rol = p_rol;

        lectorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);

        lectorEN.NumModificaciones = p_numModificaciones;

        lectorEN.CantLibrosCurso = p_cantLibrosCurso;

        lectorEN.CantLibrosLeidos = p_cantLibrosLeidos;

        lectorEN.CantAutoresSeguidos = p_cantAutoresSeguidos;

        lectorEN.CantClubsSuscritos = p_cantClubsSuscritos;



        oid = _ILectorRepository.CrearLector (lectorEN);
        return oid;
}

public void ModificarLector (int p_Lector_OID, string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass, int p_numModificaciones, int p_cantLibrosCurso, int p_cantLibrosLeidos, int p_cantAutoresSeguidos, int p_cantClubsSuscritos)
{
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
        lectorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        lectorEN.NumModificaciones = p_numModificaciones;
        lectorEN.CantLibrosCurso = p_cantLibrosCurso;
        lectorEN.CantLibrosLeidos = p_cantLibrosLeidos;
        lectorEN.CantAutoresSeguidos = p_cantAutoresSeguidos;
        lectorEN.CantClubsSuscritos = p_cantClubsSuscritos;
        //Call to LectorRepository

        _ILectorRepository.ModificarLector (lectorEN);
}

public LectorEN DameLectorPorOID (int id
                                  )
{
        LectorEN lectorEN = null;

        lectorEN = _ILectorRepository.DameLectorPorOID (id);
        return lectorEN;
}

public System.Collections.Generic.IList<LectorEN> DameTodosLectores (int first, int size)
{
        System.Collections.Generic.IList<LectorEN> list = null;

        list = _ILectorRepository.DameTodosLectores (first, size);
        return list;
}
}
}
