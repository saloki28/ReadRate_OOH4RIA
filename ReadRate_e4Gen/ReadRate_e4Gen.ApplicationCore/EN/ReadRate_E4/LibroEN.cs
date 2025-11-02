
using System;
// Definición clase LibroEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class LibroEN
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
 *	Atributo genero
 */
private string genero;



/**
 *	Atributo edadRecomendada
 */
private int edadRecomendada;



/**
 *	Atributo fechaPublicacion
 */
private Nullable<DateTime> fechaPublicacion;



/**
 *	Atributo numPags
 */
private int numPags;



/**
 *	Atributo sinopsis
 */
private string sinopsis;



/**
 *	Atributo fotoPortada
 */
private string fotoPortada;



/**
 *	Atributo lectorGuardando
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorGuardando;



/**
 *	Atributo lectorLeyendo
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorLeyendo;



/**
 *	Atributo autorPublicador
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorPublicador;



/**
 *	Atributo reseña
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> reseña;



/**
 *	Atributo valoracionMedia
 */
private float valoracionMedia;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Titulo {
        get { return titulo; } set { titulo = value;  }
}



public virtual string Genero {
        get { return genero; } set { genero = value;  }
}



public virtual int EdadRecomendada {
        get { return edadRecomendada; } set { edadRecomendada = value;  }
}



public virtual Nullable<DateTime> FechaPublicacion {
        get { return fechaPublicacion; } set { fechaPublicacion = value;  }
}



public virtual int NumPags {
        get { return numPags; } set { numPags = value;  }
}



public virtual string Sinopsis {
        get { return sinopsis; } set { sinopsis = value;  }
}



public virtual string FotoPortada {
        get { return fotoPortada; } set { fotoPortada = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> LectorGuardando {
        get { return lectorGuardando; } set { lectorGuardando = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> LectorLeyendo {
        get { return lectorLeyendo; } set { lectorLeyendo = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN AutorPublicador {
        get { return autorPublicador; } set { autorPublicador = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> Reseña {
        get { return reseña; } set { reseña = value;  }
}



public virtual float ValoracionMedia {
        get { return valoracionMedia; } set { valoracionMedia = value;  }
}





public LibroEN()
{
        lectorGuardando = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
        lectorLeyendo = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
        reseña = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN>();
}



public LibroEN(int id, string titulo, string genero, int edadRecomendada, Nullable<DateTime> fechaPublicacion, int numPags, string sinopsis, string fotoPortada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorGuardando, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorLeyendo, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorPublicador, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> reseña, float valoracionMedia
               )
{
        this.init (Id, titulo, genero, edadRecomendada, fechaPublicacion, numPags, sinopsis, fotoPortada, lectorGuardando, lectorLeyendo, autorPublicador, reseña, valoracionMedia);
}


public LibroEN(LibroEN libro)
{
        this.init (libro.Id, libro.Titulo, libro.Genero, libro.EdadRecomendada, libro.FechaPublicacion, libro.NumPags, libro.Sinopsis, libro.FotoPortada, libro.LectorGuardando, libro.LectorLeyendo, libro.AutorPublicador, libro.Reseña, libro.ValoracionMedia);
}

private void init (int id
                   , string titulo, string genero, int edadRecomendada, Nullable<DateTime> fechaPublicacion, int numPags, string sinopsis, string fotoPortada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorGuardando, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorLeyendo, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorPublicador, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> reseña, float valoracionMedia)
{
        this.Id = id;


        this.Titulo = titulo;

        this.Genero = genero;

        this.EdadRecomendada = edadRecomendada;

        this.FechaPublicacion = fechaPublicacion;

        this.NumPags = numPags;

        this.Sinopsis = sinopsis;

        this.FotoPortada = fotoPortada;

        this.LectorGuardando = lectorGuardando;

        this.LectorLeyendo = lectorLeyendo;

        this.AutorPublicador = autorPublicador;

        this.Reseña = reseña;

        this.ValoracionMedia = valoracionMedia;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        LibroEN t = obj as LibroEN;
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
