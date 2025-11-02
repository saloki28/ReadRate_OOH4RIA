
using System;
// Definición clase ReseñaEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class ReseñaEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo textoOpinion
 */
private string textoOpinion;



/**
 *	Atributo valoracion
 */
private float valoracion;



/**
 *	Atributo lectorValorador
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorValorador;



/**
 *	Atributo libroReseñado
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroReseñado;



/**
 *	Atributo notificacionReseña
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionReseña;



/**
 *	Atributo fecha
 */
private Nullable<DateTime> fecha;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string TextoOpinion {
        get { return textoOpinion; } set { textoOpinion = value;  }
}



public virtual float Valoracion {
        get { return valoracion; } set { valoracion = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN LectorValorador {
        get { return lectorValorador; } set { lectorValorador = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN LibroReseñado {
        get { return libroReseñado; } set { libroReseñado = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN NotificacionReseña {
        get { return notificacionReseña; } set { notificacionReseña = value;  }
}



public virtual Nullable<DateTime> Fecha {
        get { return fecha; } set { fecha = value;  }
}





public ReseñaEN()
{
}



public ReseñaEN(int id, string textoOpinion, float valoracion, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorValorador, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroReseñado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionReseña, Nullable<DateTime> fecha
                )
{
        this.init (Id, textoOpinion, valoracion, lectorValorador, libroReseñado, notificacionReseña, fecha);
}


public ReseñaEN(ReseñaEN reseña)
{
        this.init (reseña.Id, reseña.TextoOpinion, reseña.Valoracion, reseña.LectorValorador, reseña.LibroReseñado, reseña.NotificacionReseña, reseña.Fecha);
}

private void init (int id
                   , string textoOpinion, float valoracion, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorValorador, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroReseñado, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionReseña, Nullable<DateTime> fecha)
{
        this.Id = id;


        this.TextoOpinion = textoOpinion;

        this.Valoracion = valoracion;

        this.LectorValorador = lectorValorador;

        this.LibroReseñado = libroReseñado;

        this.NotificacionReseña = notificacionReseña;

        this.Fecha = fecha;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        ReseñaEN t = obj as ReseñaEN;
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
