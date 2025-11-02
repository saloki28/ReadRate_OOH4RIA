
using System;
// Definición clase AdministradorEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class AdministradorEN
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
 *	Atributo noticiaAdministrada
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> noticiaAdministrada;



/**
 *	Atributo eventoAdministrado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> eventoAdministrado;



/**
 *	Atributo pass
 */
private String pass;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Nombre {
        get { return nombre; } set { nombre = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> NoticiaAdministrada {
        get { return noticiaAdministrada; } set { noticiaAdministrada = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> EventoAdministrado {
        get { return eventoAdministrado; } set { eventoAdministrado = value;  }
}



public virtual String Pass {
        get { return pass; } set { pass = value;  }
}





public AdministradorEN()
{
        noticiaAdministrada = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN>();
        eventoAdministrado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN>();
}



public AdministradorEN(int id, string nombre, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> noticiaAdministrada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> eventoAdministrado, String pass
                       )
{
        this.init (Id, nombre, noticiaAdministrada, eventoAdministrado, pass);
}


public AdministradorEN(AdministradorEN administrador)
{
        this.init (administrador.Id, administrador.Nombre, administrador.NoticiaAdministrada, administrador.EventoAdministrado, administrador.Pass);
}

private void init (int id
                   , string nombre, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> noticiaAdministrada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> eventoAdministrado, String pass)
{
        this.Id = id;


        this.Nombre = nombre;

        this.NoticiaAdministrada = noticiaAdministrada;

        this.EventoAdministrado = eventoAdministrado;

        this.Pass = pass;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        AdministradorEN t = obj as AdministradorEN;
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
