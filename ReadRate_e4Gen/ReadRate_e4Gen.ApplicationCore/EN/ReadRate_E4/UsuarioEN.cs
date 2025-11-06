
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
 *	Atributo evento
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> evento;



/**
 *	Atributo notificacion
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacion;



/**
 *	Atributo clubSuscrito
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscrito;



/**
 *	Atributo clubCreado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado;



/**
 *	Atributo pass
 */
private String pass;






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



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> Evento {
        get { return evento; } set { evento = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> Notificacion {
        get { return notificacion; } set { notificacion = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> ClubSuscrito {
        get { return clubSuscrito; } set { clubSuscrito = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> ClubCreado {
        get { return clubCreado; } set { clubCreado = value;  }
}



public virtual String Pass {
        get { return pass; } set { pass = value;  }
}





public UsuarioEN()
{
        evento = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN>();
        notificacion = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
        clubSuscrito = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN>();
        clubCreado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN>();
}



public UsuarioEN(int id, string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> evento, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacion, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscrito, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado, String pass
                 )
{
        this.init (Id, email, nombreUsuario, fechaNacimiento, ciudadResidencia, paisResidencia, foto, rol, evento, notificacion, clubSuscrito, clubCreado, pass);
}


public UsuarioEN(UsuarioEN usuario)
{
        this.init (usuario.Id, usuario.Email, usuario.NombreUsuario, usuario.FechaNacimiento, usuario.CiudadResidencia, usuario.PaisResidencia, usuario.Foto, usuario.Rol, usuario.Evento, usuario.Notificacion, usuario.ClubSuscrito, usuario.ClubCreado, usuario.Pass);
}

private void init (int id
                   , string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> evento, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacion, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscrito, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado, String pass)
{
        this.Id = id;


        this.Email = email;

        this.NombreUsuario = nombreUsuario;

        this.FechaNacimiento = fechaNacimiento;

        this.CiudadResidencia = ciudadResidencia;

        this.PaisResidencia = paisResidencia;

        this.Foto = foto;

        this.Rol = rol;

        this.Evento = evento;

        this.Notificacion = notificacion;

        this.ClubSuscrito = clubSuscrito;

        this.ClubCreado = clubCreado;

        this.Pass = pass;
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
