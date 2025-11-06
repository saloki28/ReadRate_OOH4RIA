
using System;
// Definición clase NoticiaEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class NoticiaEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo titulo
 */
private string titulo;



/**
 *	Atributo fechaPublicacion
 */
private Nullable<DateTime> fechaPublicacion;



/**
 *	Atributo foto
 */
private string foto;



/**
 *	Atributo textoContenido
 */
private string textoContenido;



/**
 *	Atributo administradorNoticias
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN administradorNoticias;



/**
 *	Atributo notificacionNoticia
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionNoticia;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Titulo {
        get { return titulo; } set { titulo = value;  }
}



public virtual Nullable<DateTime> FechaPublicacion {
        get { return fechaPublicacion; } set { fechaPublicacion = value;  }
}



public virtual string Foto {
        get { return foto; } set { foto = value;  }
}



public virtual string TextoContenido {
        get { return textoContenido; } set { textoContenido = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN AdministradorNoticias {
        get { return administradorNoticias; } set { administradorNoticias = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> NotificacionNoticia {
        get { return notificacionNoticia; } set { notificacionNoticia = value;  }
}





public NoticiaEN()
{
        notificacionNoticia = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
}



public NoticiaEN(int id, string titulo, Nullable<DateTime> fechaPublicacion, string foto, string textoContenido, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN administradorNoticias, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionNoticia
                 )
{
        this.init (Id, titulo, fechaPublicacion, foto, textoContenido, administradorNoticias, notificacionNoticia);
}


public NoticiaEN(NoticiaEN noticia)
{
        this.init (noticia.Id, noticia.Titulo, noticia.FechaPublicacion, noticia.Foto, noticia.TextoContenido, noticia.AdministradorNoticias, noticia.NotificacionNoticia);
}

private void init (int id
                   , string titulo, Nullable<DateTime> fechaPublicacion, string foto, string textoContenido, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN administradorNoticias, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionNoticia)
{
        this.Id = id;


        this.Titulo = titulo;

        this.FechaPublicacion = fechaPublicacion;

        this.Foto = foto;

        this.TextoContenido = textoContenido;

        this.AdministradorNoticias = administradorNoticias;

        this.NotificacionNoticia = notificacionNoticia;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        NoticiaEN t = obj as NoticiaEN;
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
