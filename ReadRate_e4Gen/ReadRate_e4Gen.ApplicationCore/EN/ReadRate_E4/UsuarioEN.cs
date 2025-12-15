
using System;
// Definición clase UsuarioEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class UsuarioEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo email
 */
private string email;



/**
 *	Atributo nombreUsuario
 */
private string nombreUsuario;



/**
 *	Atributo fechaNacimiento
 */
private Nullable<DateTime> fechaNacimiento;



/**
 *	Atributo ciudadResidencia
 */
private string ciudadResidencia;



/**
 *	Atributo paisResidencia
 */
private string paisResidencia;



/**
 *	Atributo foto
 */
private string foto;



/**
 *	Atributo rol
 */
private ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol;



/**
 *	Atributo pass
 */
private String pass;



/**
 *	Atributo numModificaciones
 */
private int numModificaciones;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Email {
        get { return email; } set { email = value;  }
}



public virtual string NombreUsuario {
        get { return nombreUsuario; } set { nombreUsuario = value;  }
}



public virtual Nullable<DateTime> FechaNacimiento {
        get { return fechaNacimiento; } set { fechaNacimiento = value;  }
}



public virtual string CiudadResidencia {
        get { return ciudadResidencia; } set { ciudadResidencia = value;  }
}



public virtual string PaisResidencia {
        get { return paisResidencia; } set { paisResidencia = value;  }
}



public virtual string Foto {
        get { return foto; } set { foto = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum Rol {
        get { return rol; } set { rol = value;  }
}



public virtual String Pass {
        get { return pass; } set { pass = value;  }
}



public virtual int NumModificaciones {
        get { return numModificaciones; } set { numModificaciones = value;  }
}





public UsuarioEN()
{
}



public UsuarioEN(int id, string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, String pass, int numModificaciones
                 )
{
        this.init (Id, email, nombreUsuario, fechaNacimiento, ciudadResidencia, paisResidencia, foto, rol, pass, numModificaciones);
}


public UsuarioEN(UsuarioEN usuario)
{
        this.init (usuario.Id, usuario.Email, usuario.NombreUsuario, usuario.FechaNacimiento, usuario.CiudadResidencia, usuario.PaisResidencia, usuario.Foto, usuario.Rol, usuario.Pass, usuario.NumModificaciones);
}

private void init (int id
                   , string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, String pass, int numModificaciones)
{
        this.Id = id;


        this.Email = email;

        this.NombreUsuario = nombreUsuario;

        this.FechaNacimiento = fechaNacimiento;

        this.CiudadResidencia = ciudadResidencia;

        this.PaisResidencia = paisResidencia;

        this.Foto = foto;

        this.Rol = rol;

        this.Pass = pass;

        this.NumModificaciones = numModificaciones;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        UsuarioEN t = obj as UsuarioEN;
        if (t == null)
                return false;
        if (Id.Equals (t.Id))
                return true;
        else
                return false;
}

public override int GetHashCode ()
{
        int hash = 13;

        hash += this.Id.GetHashCode ();
        return hash;
}
}
}
