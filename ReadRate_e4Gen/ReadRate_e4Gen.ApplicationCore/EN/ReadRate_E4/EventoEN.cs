
using System;
// Definición clase EventoEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class EventoEN
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
 *	Atributo foto
 */
private string foto;



/**
 *	Atributo descripcion
 */
private string descripcion;



/**
 *	Atributo fecha
 */
private Nullable<DateTime> fecha;



/**
 *	Atributo hora
 */
private Nullable<DateTime> hora;



/**
 *	Atributo ubicacion
 */
private string ubicacion;



/**
 *	Atributo aforoMax
 */
private int aforoMax;



/**
 *	Atributo administradorEventos
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN administradorEventos;



/**
 *	Atributo notificacionEvento
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionEvento;



/**
 *	Atributo aforoActual
 */
private int aforoActual;



/**
 *	Atributo autorParticipante
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorParticipante;



/**
 *	Atributo lectorParticipante
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorParticipante;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Nombre {
        get { return nombre; } set { nombre = value;  }
}



public virtual string Foto {
        get { return foto; } set { foto = value;  }
}



public virtual string Descripcion {
        get { return descripcion; } set { descripcion = value;  }
}



public virtual Nullable<DateTime> Fecha {
        get { return fecha; } set { fecha = value;  }
}



public virtual Nullable<DateTime> Hora {
        get { return hora; } set { hora = value;  }
}



public virtual string Ubicacion {
        get { return ubicacion; } set { ubicacion = value;  }
}



public virtual int AforoMax {
        get { return aforoMax; } set { aforoMax = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN AdministradorEventos {
        get { return administradorEventos; } set { administradorEventos = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> NotificacionEvento {
        get { return notificacionEvento; } set { notificacionEvento = value;  }
}



public virtual int AforoActual {
        get { return aforoActual; } set { aforoActual = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> AutorParticipante {
        get { return autorParticipante; } set { autorParticipante = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> LectorParticipante {
        get { return lectorParticipante; } set { lectorParticipante = value;  }
}





public EventoEN()
{
        notificacionEvento = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
        autorParticipante = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN>();
        lectorParticipante = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
}



public EventoEN(int id, string nombre, string foto, string descripcion, Nullable<DateTime> fecha, Nullable<DateTime> hora, string ubicacion, int aforoMax, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN administradorEventos, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionEvento, int aforoActual, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorParticipante, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorParticipante
                )
{
        this.init (Id, nombre, foto, descripcion, fecha, hora, ubicacion, aforoMax, administradorEventos, notificacionEvento, aforoActual, autorParticipante, lectorParticipante);
}


public EventoEN(EventoEN evento)
{
        this.init (evento.Id, evento.Nombre, evento.Foto, evento.Descripcion, evento.Fecha, evento.Hora, evento.Ubicacion, evento.AforoMax, evento.AdministradorEventos, evento.NotificacionEvento, evento.AforoActual, evento.AutorParticipante, evento.LectorParticipante);
}

private void init (int id
                   , string nombre, string foto, string descripcion, Nullable<DateTime> fecha, Nullable<DateTime> hora, string ubicacion, int aforoMax, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN administradorEventos, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionEvento, int aforoActual, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorParticipante, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorParticipante)
{
        this.Id = id;


        this.Nombre = nombre;

        this.Foto = foto;

        this.Descripcion = descripcion;

        this.Fecha = fecha;

        this.Hora = hora;

        this.Ubicacion = ubicacion;

        this.AforoMax = aforoMax;

        this.AdministradorEventos = administradorEventos;

        this.NotificacionEvento = notificacionEvento;

        this.AforoActual = aforoActual;

        this.AutorParticipante = autorParticipante;

        this.LectorParticipante = lectorParticipante;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        EventoEN t = obj as EventoEN;
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
