
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
 *	Atributo lectorPropietario
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorPropietario;



/**
 *	Atributo notificacionClub
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionClub;



/**
 *	Atributo miembrosActuales
 */
private int miembrosActuales;



/**
 *	Atributo lectorMiembro
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorMiembro;






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



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN LectorPropietario {
        get { return lectorPropietario; } set { lectorPropietario = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> NotificacionClub {
        get { return notificacionClub; } set { notificacionClub = value;  }
}



public virtual int MiembrosActuales {
        get { return miembrosActuales; } set { miembrosActuales = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> LectorMiembro {
        get { return lectorMiembro; } set { lectorMiembro = value;  }
}





public ClubEN()
{
        notificacionClub = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
        lectorMiembro = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
}



public ClubEN(int id, string nombre, string enlaceDiscord, int miembrosMax, string foto, string descripcion, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorPropietario, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionClub, int miembrosActuales, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorMiembro
              )
{
        this.init (Id, nombre, enlaceDiscord, miembrosMax, foto, descripcion, lectorPropietario, notificacionClub, miembrosActuales, lectorMiembro);
}


public ClubEN(ClubEN club)
{
        this.init (club.Id, club.Nombre, club.EnlaceDiscord, club.MiembrosMax, club.Foto, club.Descripcion, club.LectorPropietario, club.NotificacionClub, club.MiembrosActuales, club.LectorMiembro);
}

private void init (int id
                   , string nombre, string enlaceDiscord, int miembrosMax, string foto, string descripcion, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorPropietario, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionClub, int miembrosActuales, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorMiembro)
{
        this.Id = id;


        this.Nombre = nombre;

        this.EnlaceDiscord = enlaceDiscord;

        this.MiembrosMax = miembrosMax;

        this.Foto = foto;

        this.Descripcion = descripcion;

        this.LectorPropietario = lectorPropietario;

        this.NotificacionClub = notificacionClub;

        this.MiembrosActuales = miembrosActuales;

        this.LectorMiembro = lectorMiembro;
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
