

using System;
using System.Text;
using System.Collections.Generic;

using ReadRate_e4Gen.ApplicationCore.Exceptions;

using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
/*
 *      Definition of the class AutorCEN
 *
 */
public partial class AutorCEN
{
private IAutorRepository _IAutorRepository;

public AutorCEN(IAutorRepository _IAutorRepository)
{
        this._IAutorRepository = _IAutorRepository;
}

public IAutorRepository get_IAutorRepository ()
{
        return this._IAutorRepository;
}

public int CrearAutor (string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass, int p_numeroSeguidores, int p_cantidadLibrosPublicados, float p_valoracionMedia)
{
        AutorEN autorEN = null;
        int oid;

        //Initialized AutorEN
        autorEN = new AutorEN ();
        autorEN.Email = p_email;

        autorEN.NombreUsuario = p_nombreUsuario;

        autorEN.FechaNacimiento = p_fechaNacimiento;

        autorEN.CiudadResidencia = p_ciudadResidencia;

        autorEN.PaisResidencia = p_paisResidencia;

        autorEN.Foto = p_foto;

        autorEN.Rol = p_rol;

        autorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);

        autorEN.NumeroSeguidores = p_numeroSeguidores;

        autorEN.CantidadLibrosPublicados = p_cantidadLibrosPublicados;

        autorEN.ValoracionMedia = p_valoracionMedia;



        oid = _IAutorRepository.CrearAutor (autorEN);
        return oid;
}

public void ModificarAutor (int p_Autor_OID, string p_email, string p_nombreUsuario, Nullable<DateTime> p_fechaNacimiento, string p_ciudadResidencia, string p_paisResidencia, string p_foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum p_rol, String p_pass, int p_numeroSeguidores, int p_cantidadLibrosPublicados, float p_valoracionMedia)
{
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
        autorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        autorEN.NumeroSeguidores = p_numeroSeguidores;
        autorEN.CantidadLibrosPublicados = p_cantidadLibrosPublicados;
        autorEN.ValoracionMedia = p_valoracionMedia;
        //Call to AutorRepository

        _IAutorRepository.ModificarAutor (autorEN);
}

public void EliminarAutor (int id
                           )
{
        _IAutorRepository.EliminarAutor (id);
}

public AutorEN DameAutorPorOID (int id
                                )
{
        AutorEN autorEN = null;

        autorEN = _IAutorRepository.DameAutorPorOID (id);
        return autorEN;
}

public System.Collections.Generic.IList<AutorEN> DameTodosAutores (int first, int size)
{
        System.Collections.Generic.IList<AutorEN> list = null;

        list = _IAutorRepository.DameTodosAutores (first, size);
        return list;
}
}
}
