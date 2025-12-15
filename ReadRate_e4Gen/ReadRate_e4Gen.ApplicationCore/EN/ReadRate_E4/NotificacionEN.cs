
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
 *	Atributo reseñaNotificada
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN reseñaNotificada;



/**
 *	Atributo autorNotificado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorNotificado;



/**
 *	Atributo lectorNotificado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorNotificado;



/**
 *	Atributo tituloResumen
 */
private string tituloResumen;



/**
 *	Atributo textoCuerpo
 */
private string textoCuerpo;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual Nullable<DateTime> Fecha {
        get { return fecha; } set { fecha = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum Concepto {
        get { return concepto; } set { concepto = value;  }
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



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN ReseñaNotificada {
        get { return reseñaNotificada; } set { reseñaNotificada = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> AutorNotificado {
        get { return autorNotificado; } set { autorNotificado = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> LectorNotificado {
        get { return lectorNotificado; } set { lectorNotificado = value;  }
}



public virtual string TituloResumen {
        get { return tituloResumen; } set { tituloResumen = value;  }
}



public virtual string TextoCuerpo {
        get { return textoCuerpo; } set { textoCuerpo = value;  }
}





public NotificacionEN()
{
        autorNotificado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN>();
        lectorNotificado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
}



public NotificacionEN(int id, Nullable<DateTime> fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum concepto, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN noticiaNotificada, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN reseñaNotificada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorNotificado, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorNotificado, string tituloResumen, string textoCuerpo
                      )
{
        this.init (Id, fecha, concepto, noticiaNotificada, eventoNotificado, clubNotificado, reseñaNotificada, autorNotificado, lectorNotificado, tituloResumen, textoCuerpo);
}


public NotificacionEN(NotificacionEN notificacion)
{
        this.init (notificacion.Id, notificacion.Fecha, notificacion.Concepto, notificacion.NoticiaNotificada, notificacion.EventoNotificado, notificacion.ClubNotificado, notificacion.ReseñaNotificada, notificacion.AutorNotificado, notificacion.LectorNotificado, notificacion.TituloResumen, notificacion.TextoCuerpo);
}

private void init (int id
                   , Nullable<DateTime> fecha, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.ConceptoNotificacionEnum concepto, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN noticiaNotificada, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubNotificado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN reseñaNotificada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorNotificado, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorNotificado, string tituloResumen, string textoCuerpo)
{
        this.Id = id;


        this.Fecha = fecha;

        this.Concepto = concepto;

        this.NoticiaNotificada = noticiaNotificada;

        this.EventoNotificado = eventoNotificado;

        this.ClubNotificado = clubNotificado;

        this.ReseñaNotificada = reseñaNotificada;

        this.AutorNotificado = autorNotificado;

        this.LectorNotificado = lectorNotificado;

        this.TituloResumen = tituloResumen;

        this.TextoCuerpo = textoCuerpo;
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
