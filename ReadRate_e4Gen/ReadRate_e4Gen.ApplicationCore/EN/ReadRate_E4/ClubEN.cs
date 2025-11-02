
using System;
// Definición clase ClubEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class ClubEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo nombre
 */
private string nombre;



/**
 *	Atributo enlaceDiscord
 */
private string enlaceDiscord;



/**
 *	Atributo miembrosMax
 */
private int miembrosMax;



/**
 *	Atributo foto
 */
private string foto;



/**
 *	Atributo descripcion
 */
private string descripcion;



/**
 *	Atributo usuarioMiembro
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> usuarioMiembro;



/**
 *	Atributo usuarioPropietario
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioPropietario;



/**
 *	Atributo notificacionClub
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionClub;



/**
 *	Atributo miembrosActuales
 */
private int miembrosActuales;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Nombre {
        get { return nombre; } set { nombre = value;  }
}



public virtual string EnlaceDiscord {
        get { return enlaceDiscord; } set { enlaceDiscord = value;  }
}



public virtual int MiembrosMax {
        get { return miembrosMax; } set { miembrosMax = value;  }
}



public virtual string Foto {
        get { return foto; } set { foto = value;  }
}



public virtual string Descripcion {
        get { return descripcion; } set { descripcion = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> UsuarioMiembro {
        get { return usuarioMiembro; } set { usuarioMiembro = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN UsuarioPropietario {
        get { return usuarioPropietario; } set { usuarioPropietario = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> NotificacionClub {
        get { return notificacionClub; } set { notificacionClub = value;  }
}



public virtual int MiembrosActuales {
        get { return miembrosActuales; } set { miembrosActuales = value;  }
}





public ClubEN()
{
        usuarioMiembro = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN>();
        notificacionClub = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
}



public ClubEN(int id, string nombre, string enlaceDiscord, int miembrosMax, string foto, string descripcion, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> usuarioMiembro, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioPropietario, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionClub, int miembrosActuales
              )
{
        this.init (Id, nombre, enlaceDiscord, miembrosMax, foto, descripcion, usuarioMiembro, usuarioPropietario, notificacionClub, miembrosActuales);
}


public ClubEN(ClubEN club)
{
        this.init (club.Id, club.Nombre, club.EnlaceDiscord, club.MiembrosMax, club.Foto, club.Descripcion, club.UsuarioMiembro, club.UsuarioPropietario, club.NotificacionClub, club.MiembrosActuales);
}

private void init (int id
                   , string nombre, string enlaceDiscord, int miembrosMax, string foto, string descripcion, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> usuarioMiembro, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioPropietario, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionClub, int miembrosActuales)
{
        this.Id = id;


        this.Nombre = nombre;

        this.EnlaceDiscord = enlaceDiscord;

        this.MiembrosMax = miembrosMax;

        this.Foto = foto;

        this.Descripcion = descripcion;

        this.UsuarioMiembro = usuarioMiembro;

        this.UsuarioPropietario = usuarioPropietario;

        this.NotificacionClub = notificacionClub;

        this.MiembrosActuales = miembrosActuales;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        ClubEN t = obj as ClubEN;
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
