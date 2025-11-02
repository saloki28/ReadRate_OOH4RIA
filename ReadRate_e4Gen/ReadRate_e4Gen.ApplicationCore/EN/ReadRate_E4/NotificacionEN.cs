
using System;
// Definición clase NotificacionEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class NotificacionEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo fecha
 */
private Nullable<DateTime> fecha;



/**
 *	Atributo concepto
 */
private ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum concepto;



/**
 *	Atributo usuarioNotificado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> usuarioNotificado;



/**
 *	Atributo noticiaNotificada
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN noticiaNotificada;



/**
 *	Atributo eventoNotificado
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoNotificado;



/**
 *	Atributo clubNotificado
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubNotificado;



/**
 *	Atributo autorAvisado
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorAvisado;



/**
 *	Atributo reseñaNotificada
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN reseñaNotificada;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual Nullable<DateTime> Fecha {
        get { return fecha; } set { fecha = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum Concepto {
        get { return concepto; } set { concepto = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> UsuarioNotificado {
        get { return usuarioNotificado; } set { usuarioNotificado = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN NoticiaNotificada {
        get { return noticiaNotificada; } set { noticiaNotificada = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN EventoNotificado {
        get { return eventoNotificado; } set { eventoNotificado = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN ClubNotificado {
        get { return clubNotificado; } set { clubNotificado = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN AutorAvisado {
        get { return autorAvisado; } set { autorAvisado = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN ReseñaNotificada {
        get { return reseñaNotificada; } set { reseñaNotificada = value;  }
}





public NotificacionEN()
{
        usuarioNotificado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN>();
}



public NotificacionEN(int id, Nullable<DateTime> fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum concepto, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> usuarioNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN noticiaNotificada, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorAvisado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN reseñaNotificada
                      )
{
        this.init (Id, fecha, concepto, usuarioNotificado, noticiaNotificada, eventoNotificado, clubNotificado, autorAvisado, reseñaNotificada);
}


public NotificacionEN(NotificacionEN notificacion)
{
        this.init (notificacion.Id, notificacion.Fecha, notificacion.Concepto, notificacion.UsuarioNotificado, notificacion.NoticiaNotificada, notificacion.EventoNotificado, notificacion.ClubNotificado, notificacion.AutorAvisado, notificacion.ReseñaNotificada);
}

private void init (int id
                   , Nullable<DateTime> fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum concepto, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> usuarioNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN noticiaNotificada, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorAvisado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN reseñaNotificada)
{
        this.Id = id;


        this.Fecha = fecha;

        this.Concepto = concepto;

        this.UsuarioNotificado = usuarioNotificado;

        this.NoticiaNotificada = noticiaNotificada;

        this.EventoNotificado = eventoNotificado;

        this.ClubNotificado = clubNotificado;

        this.AutorAvisado = autorAvisado;

        this.ReseñaNotificada = reseñaNotificada;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        NotificacionEN t = obj as NotificacionEN;
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
