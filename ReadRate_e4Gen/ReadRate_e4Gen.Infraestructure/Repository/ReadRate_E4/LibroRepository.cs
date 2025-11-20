
using System;
using System.Text;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4;
using ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4;


/*
 * Clase Libro:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class LibroRepository : BasicRepository, ILibroRepository
{
public LibroRepository() : base ()
{
}


public LibroRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public LibroEN ReadOIDDefault (int id
                               )
{
        LibroEN libroEN = null;

        try
        {
                SessionInitializeTransaction ();
                libroEN = (LibroEN)session.Get (typeof(LibroNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return libroEN;
}

public System.Collections.Generic.IList<LibroEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<LibroEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(LibroNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<LibroEN>();
                        else
                                result = session.CreateCriteria (typeof(LibroNH)).List<LibroEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (LibroEN libro)
{
        try
        {
                SessionInitializeTransaction ();
                LibroNH libroNH = (LibroNH)session.Load (typeof(LibroNH), libro.Id);

                libroNH.Titulo = libro.Titulo;


                libroNH.Genero = libro.Genero;


                libroNH.EdadRecomendada = libro.EdadRecomendada;


                libroNH.FechaPublicacion = libro.FechaPublicacion;


                libroNH.NumPags = libro.NumPags;


                libroNH.Sinopsis = libro.Sinopsis;


                libroNH.FotoPortada = libro.FotoPortada;






                libroNH.ValoracionMedia = libro.ValoracionMedia;

                session.Update (libroNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearLibro (LibroEN libro)
{
        LibroNH libroNH = new LibroNH (libro);

        try
        {
                SessionInitializeTransaction ();
                if (libro.AutorPublicador != null) {
                        // Argumento OID y no colección.
                        libroNH
                        .AutorPublicador = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN), libro.AutorPublicador.Id);

                        libroNH.AutorPublicador.LibroPublicado
                        .Add (libroNH);
                }

                session.Save (libroNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return libroNH.Id;
}

public void ModificarLibro (LibroEN libro)
{
        try
        {
                SessionInitializeTransaction ();
                LibroNH libroNH = (LibroNH)session.Load (typeof(LibroNH), libro.Id);

                libroNH.Titulo = libro.Titulo;


                libroNH.Genero = libro.Genero;


                libroNH.EdadRecomendada = libro.EdadRecomendada;


                libroNH.FechaPublicacion = libro.FechaPublicacion;


                libroNH.NumPags = libro.NumPags;


                libroNH.Sinopsis = libro.Sinopsis;


                libroNH.FotoPortada = libro.FotoPortada;


                libroNH.ValoracionMedia = libro.ValoracionMedia;

                session.Update (libroNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarLibro (int id
                           )
{
        try
        {
                SessionInitializeTransaction ();
                LibroNH libroNH = (LibroNH)session.Load (typeof(LibroNH), id);
                session.Delete (libroNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameLibroPorOID
//Con e: LibroEN
public LibroEN DameLibroPorOID (int id
                                )
{
        LibroEN libroEN = null;

        try
        {
                SessionInitializeTransaction ();
                libroEN = (LibroEN)session.Get (typeof(LibroNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return libroEN;
}

public System.Collections.Generic.IList<LibroEN> DameTodosLibros (int first, int size)
{
        System.Collections.Generic.IList<LibroEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(LibroNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<LibroEN>();
                else
                        result = session.CreateCriteria (typeof(LibroNH)).List<LibroEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> DameLibrosPorFiltros (string p_genero, string p_titulo, int? p_edadRecomendada, int? p_numPags, float? p_valoracionMedia, int first, int size)
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM LibroNH self where FROM LibroNH libro where (:p_genero is null or libro.Genero=:p_genero) and (:p_edadRecomendada is null or libro.EdadRecomendada >= :p_edadRecomendada) and (:p_numPags is null or libro.NumPags >= :p_numPags) and (:p_valoracionMedia is null or libro.ValoracionMedia >= :p_valoracionMedia) and (:p_titulo is null or concat('%', lower(libro.Titulo), '%') like lower(:p_titulo))";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("LibroNHdameLibrosPorFiltrosHQL");
                query.SetParameter ("p_genero", p_genero);
                query.SetParameter ("p_titulo", p_titulo);
                query.SetParameter ("p_edadRecomendada", p_edadRecomendada);
                query.SetParameter ("p_numPags", p_numPags);
                query.SetParameter ("p_valoracionMedia", p_valoracionMedia);

                if (size > 0) {
                        query.SetFirstResult (first).SetMaxResults (size);
                }
                else{
                        query.SetFirstResult (first);
                }

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> DameLibrosOrdenadosFecha ()
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM LibroNH self where FROM LibroNH libro order by libro.FechaPublicacion desc";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("LibroNHdameLibrosOrdenadosFechaHQL");

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LibroRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
