
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
 * Clase Noticia:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class NoticiaRepository : BasicRepository, INoticiaRepository
{
public NoticiaRepository() : base ()
{
}


public NoticiaRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public NoticiaEN ReadOIDDefault (int id
                                 )
{
        NoticiaEN noticiaEN = null;

        try
        {
                SessionInitializeTransaction ();
                noticiaEN = (NoticiaEN)session.Get (typeof(NoticiaNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return noticiaEN;
}

public System.Collections.Generic.IList<NoticiaEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<NoticiaEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(NoticiaNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<NoticiaEN>();
                        else
                                result = session.CreateCriteria (typeof(NoticiaNH)).List<NoticiaEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (NoticiaEN noticia)
{
        try
        {
                SessionInitializeTransaction ();
                NoticiaNH noticiaNH = (NoticiaNH)session.Load (typeof(NoticiaNH), noticia.Id);

                noticiaNH.Titulo = noticia.Titulo;


                noticiaNH.FechaPublicacion = noticia.FechaPublicacion;


                noticiaNH.Foto = noticia.Foto;


                noticiaNH.TextoContenido = noticia.TextoContenido;



                session.Update (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearNoticia (NoticiaEN noticia)
{
        NoticiaNH noticiaNH = new NoticiaNH (noticia);

        try
        {
                SessionInitializeTransaction ();
                if (noticia.AdministradorNoticias != null) {
                        // Argumento OID y no colección.
                        noticiaNH
                        .AdministradorNoticias = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN), noticia.AdministradorNoticias.Id);

                        noticiaNH.AdministradorNoticias.NoticiaAdministrada
                        .Add (noticiaNH);
                }

                session.Save (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return noticiaNH.Id;
}

public void ModificarNoticia (NoticiaEN noticia)
{
        try
        {
                SessionInitializeTransaction ();
                NoticiaNH noticiaNH = (NoticiaNH)session.Load (typeof(NoticiaNH), noticia.Id);

                noticiaNH.Titulo = noticia.Titulo;


                noticiaNH.FechaPublicacion = noticia.FechaPublicacion;


                noticiaNH.Foto = noticia.Foto;


                noticiaNH.TextoContenido = noticia.TextoContenido;

                session.Update (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarNoticia (int id
                             )
{
        try
        {
                SessionInitializeTransaction ();
                NoticiaNH noticiaNH = (NoticiaNH)session.Load (typeof(NoticiaNH), id);
                session.Delete (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameNoticiaPorOID
//Con e: NoticiaEN
public NoticiaEN DameNoticiaPorOID (int id
                                    )
{
        NoticiaEN noticiaEN = null;

        try
        {
                SessionInitializeTransaction ();
                noticiaEN = (NoticiaEN)session.Get (typeof(NoticiaNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return noticiaEN;
}

public System.Collections.Generic.IList<NoticiaEN> DameTodosNoticias (int first, int size)
{
        System.Collections.Generic.IList<NoticiaEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(NoticiaNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<NoticiaEN>();
                else
                        result = session.CreateCriteria (typeof(NoticiaNH)).List<NoticiaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public System.Collections.Generic.IList<string> DameTodosTitulosNoticias (int first, int size)
{
        System.Collections.Generic.IList<string> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM NoticiaNH self where select noticia.Titulo FROM NoticiaNH noticia order by noticia.Titulo";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("NoticiaNHdameTodosTitulosNoticiasHQL");

                if (size > 0) {
                        query.SetFirstResult (first).SetMaxResults (size);
                }
                else{
                        query.SetFirstResult (first);
                }

                result = query.List<string>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> DameNoticiasPorTitulo ()
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM NoticiaNH self where FROM NoticiaNH noticia where lower(:p_titulo) like concat ('%', lower(noticia.Titulo), '%')";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("NoticiaNHdameNoticiasPorTituloHQL");

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
