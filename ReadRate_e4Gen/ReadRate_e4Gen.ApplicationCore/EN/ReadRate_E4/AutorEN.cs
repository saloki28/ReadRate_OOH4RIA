
using System;
// Definición clase AutorEN
namespace ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4
{
public partial class AutorEN                                                                        : ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN


{
/**
 *	Atributo numeroSeguidores
 */
private int numeroSeguidores;



/**
 *	Atributo cantidadLibrosPublicados
 */
private int cantidadLibrosPublicados;



/**
 *	Atributo valoracionMedia
 */
private float valoracionMedia;



/**
 *	Atributo lectorSeguidor
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorSeguidor;



/**
 *	Atributo libroPublicado
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroPublicado;



/**
 *	Atributo avisoReseña
 */
private System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> avisoReseña;






public virtual int NumeroSeguidores {
        get { return numeroSeguidores; } set { numeroSeguidores = value;  }
}



public virtual int CantidadLibrosPublicados {
        get { return cantidadLibrosPublicados; } set { cantidadLibrosPublicados = value;  }
}



public virtual float ValoracionMedia {
        get { return valoracionMedia; } set { valoracionMedia = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> LectorSeguidor {
        get { return lectorSeguidor; } set { lectorSeguidor = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> LibroPublicado {
        get { return libroPublicado; } set { libroPublicado = value;  }
}



public virtual System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> AvisoReseña {
        get { return avisoReseña; } set { avisoReseña = value;  }
}





public AutorEN() : base ()
{
        lectorSeguidor = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
        libroPublicado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
        avisoReseña = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN>();
}



public AutorEN(int id, int numeroSeguidores, int cantidadLibrosPublicados, float valoracionMedia, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorSeguidor, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroPublicado, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> avisoReseña
               , string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> evento, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacion, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscrito, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado, String pass
               )
{
        this.init (Id, numeroSeguidores, cantidadLibrosPublicados, valoracionMedia, lectorSeguidor, libroPublicado, avisoReseña, email, nombreUsuario, fechaNacimiento, ciudadResidencia, paisResidencia, foto, rol, evento, notificacion, clubSuscrito, clubCreado, pass);
}


public AutorEN(AutorEN autor)
{
        this.init (autor.Id, autor.NumeroSeguidores, autor.CantidadLibrosPublicados, autor.ValoracionMedia, autor.LectorSeguidor, autor.LibroPublicado, autor.AvisoReseña, autor.Email, autor.NombreUsuario, autor.FechaNacimiento, autor.CiudadResidencia, autor.PaisResidencia, autor.Foto, autor.Rol, autor.Evento, autor.Notificacion, autor.ClubSuscrito, autor.ClubCreado, autor.Pass);
}

private void init (int id
                   , int numeroSeguidores, int cantidadLibrosPublicados, float valoracionMedia, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN> lectorSeguidor, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> libroPublicado, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> avisoReseña, string email, string nombreUsuario, Nullable<DateTime> fechaNacimiento, string ciudadResidencia, string paisResidencia, string foto, ReadRate_e4Gen.ApplicationCore.Enumerated.ReadRate_E4.RolUsuarioEnum rol, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> evento, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN> notificacion, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubSuscrito, System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> clubCreado, String pass)
{
        this.Id = id;


        this.NumeroSeguidores = numeroSeguidores;

        this.CantidadLibrosPublicados = cantidadLibrosPublicados;

        this.ValoracionMedia = valoracionMedia;

        this.LectorSeguidor = lectorSeguidor;

        this.LibroPublicado = libroPublicado;

        this.AvisoReseña = avisoReseña;

        this.Email = email;

        this.NombreUsuario = nombreUsuario;

        this.FechaNacimiento = fechaNacimiento;

        this.CiudadResidencia = ciudadResidencia;

        this.PaisResidencia = paisResidencia;

        this.Foto = foto;

        this.Rol = rol;

        this.Evento = evento;

        this.Notificacion = notificacion;

        this.ClubSuscrito = clubSuscrito;

        this.ClubCreado = clubCreado;

        this.Pass = pass;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        AutorEN t = obj as AutorEN;
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
