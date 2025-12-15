
using System;
// Definición clase LectorEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class LectorEN                                                                       : ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN


{
/**
 *	Atributo cantLibrosCurso
 */
private int cantLibrosCurso;



/**
 *	Atributo cantLibrosLeidos
 */
private int cantLibrosLeidos;



/**
 *	Atributo cantAutoresSeguidos
 */
private int cantAutoresSeguidos;



/**
 *	Atributo cantClubsSuscritos
 */
private int cantClubsSuscritos;



/**
 *	Atributo libroLeido
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroLeido;



/**
 *	Atributo libroEnCurso
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroEnCurso;



/**
 *	Atributo autorSeguido
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorSeguido;



/**
 *	Atributo reseñaPublicada
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> reseñaPublicada;



/**
 *	Atributo eventoLector
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> eventoLector;



/**
 *	Atributo clubSuscritoLector
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscritoLector;



/**
 *	Atributo notificacionLector
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionLector;



/**
 *	Atributo clubCreado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado;






public virtual int CantLibrosCurso {
        get { return cantLibrosCurso; } set { cantLibrosCurso = value;  }
}



public virtual int CantLibrosLeidos {
        get { return cantLibrosLeidos; } set { cantLibrosLeidos = value;  }
}



public virtual int CantAutoresSeguidos {
        get { return cantAutoresSeguidos; } set { cantAutoresSeguidos = value;  }
}



public virtual int CantClubsSuscritos {
        get { return cantClubsSuscritos; } set { cantClubsSuscritos = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> LibroLeido {
        get { return libroLeido; } set { libroLeido = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> LibroEnCurso {
        get { return libroEnCurso; } set { libroEnCurso = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> AutorSeguido {
        get { return autorSeguido; } set { autorSeguido = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> ReseñaPublicada {
        get { return reseñaPublicada; } set { reseñaPublicada = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> EventoLector {
        get { return eventoLector; } set { eventoLector = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> ClubSuscritoLector {
        get { return clubSuscritoLector; } set { clubSuscritoLector = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> NotificacionLector {
        get { return notificacionLector; } set { notificacionLector = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> ClubCreado {
        get { return clubCreado; } set { clubCreado = value;  }
}





public LectorEN() : base ()
{
        libroLeido = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
        libroEnCurso = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
        autorSeguido = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN>();
        reseñaPublicada = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN>();
        eventoLector = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN>();
        clubSuscritoLector = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN>();
        notificacionLector = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
        clubCreado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN>();
}



public LectorEN(int id, int cantLibrosCurso, int cantLibrosLeidos, int cantAutoresSeguidos, int cantClubsSuscritos, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroLeido, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroEnCurso, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorSeguido, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> reseñaPublicada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> eventoLector, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscritoLector, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionLector, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado
                , string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, String pass, int numModificaciones
                )
{
        this.init (Id, cantLibrosCurso, cantLibrosLeidos, cantAutoresSeguidos, cantClubsSuscritos, libroLeido, libroEnCurso, autorSeguido, reseñaPublicada, eventoLector, clubSuscritoLector, notificacionLector, clubCreado, email, nombreUsuario, fechaNacimiento, ciudadResidencia, paisResidencia, foto, rol, pass, numModificaciones);
}


public LectorEN(LectorEN lector)
{
        this.init (lector.Id, lector.CantLibrosCurso, lector.CantLibrosLeidos, lector.CantAutoresSeguidos, lector.CantClubsSuscritos, lector.LibroLeido, lector.LibroEnCurso, lector.AutorSeguido, lector.ReseñaPublicada, lector.EventoLector, lector.ClubSuscritoLector, lector.NotificacionLector, lector.ClubCreado, lector.Email, lector.NombreUsuario, lector.FechaNacimiento, lector.CiudadResidencia, lector.PaisResidencia, lector.Foto, lector.Rol, lector.Pass, lector.NumModificaciones);
}

private void init (int id
                   , int cantLibrosCurso, int cantLibrosLeidos, int cantAutoresSeguidos, int cantClubsSuscritos, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroLeido, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroEnCurso, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN> autorSeguido, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> reseñaPublicada, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> eventoLector, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscritoLector, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacionLector, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado, string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, String pass, int numModificaciones)
{
        this.Id = id;


        this.CantLibrosCurso = cantLibrosCurso;

        this.CantLibrosLeidos = cantLibrosLeidos;

        this.CantAutoresSeguidos = cantAutoresSeguidos;

        this.CantClubsSuscritos = cantClubsSuscritos;

        this.LibroLeido = libroLeido;

        this.LibroEnCurso = libroEnCurso;

        this.AutorSeguido = autorSeguido;

        this.ReseñaPublicada = reseñaPublicada;

        this.EventoLector = eventoLector;

        this.ClubSuscritoLector = clubSuscritoLector;

        this.NotificacionLector = notificacionLector;

        this.ClubCreado = clubCreado;

        this.Email = email;

        this.NombreUsuario = nombreUsuario;

        this.FechaNacimiento = fechaNacimiento;

        this.CiudadResidencia = ciudadResidencia;

        this.PaisResidencia = paisResidencia;

        this.Foto = foto;

        this.Rol = rol;

        this.Pass = pass;

        this.NumModificaciones = numModificaciones;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        LectorEN t = obj as LectorEN;
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
