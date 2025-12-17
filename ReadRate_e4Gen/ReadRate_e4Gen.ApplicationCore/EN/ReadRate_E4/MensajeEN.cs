
using System;
// Definición clase MensajeEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class MensajeEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo texto
 */
private string texto;



/**
 *	Atributo fecha
 */
private Nullable<DateTime> fecha;



/**
 *	Atributo lector
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lector;



/**
 *	Atributo club
 */
private ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN club;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Texto {
        get { return texto; } set { texto = value;  }
}



public virtual Nullable<DateTime> Fecha {
        get { return fecha; } set { fecha = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN Lector {
        get { return lector; } set { lector = value;  }
}



public virtual ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN Club {
        get { return club; } set { club = value;  }
}





public MensajeEN()
{
}



public MensajeEN(int id, string texto, Nullable<DateTime> fecha, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lector, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN club
                 )
{
        this.init (Id, texto, fecha, lector, club);
}


public MensajeEN(MensajeEN mensaje)
{
        this.init (mensaje.Id, mensaje.Texto, mensaje.Fecha, mensaje.Lector, mensaje.Club);
}

private void init (int id
                   , string texto, Nullable<DateTime> fecha, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lector, ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN club)
{
        this.Id = id;


        this.Texto = texto;

        this.Fecha = fecha;

        this.Lector = lector;

        this.Club = club;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        MensajeEN t = obj as MensajeEN;
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
