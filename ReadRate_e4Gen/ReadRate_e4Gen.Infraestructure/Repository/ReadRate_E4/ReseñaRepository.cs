
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
 * Clase Reseña:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class ReseñaRepository : BasicRepository, IReseñaRepository
{
public ReseñaRepository() : base ()
{
}


public ReseñaRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public ReseñaEN ReadOIDDefault (int id
                                )
{
        ReseñaEN reseñaEN = null;

        try
        {
                SessionInitializeTransaction ();
                reseñaEN = (ReseñaEN)session.Get (typeof(ReseñaNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return reseñaEN;
}

public System.Collections.Generic.IList<ReseñaEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<ReseñaEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(ReseñaNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<ReseñaEN>();
                        else
                                result = session.CreateCriteria (typeof(ReseñaNH)).List<ReseñaEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (ReseñaEN reseña)
{
        try
        {
                SessionInitializeTransaction ();
                ReseñaNH reseñaNH = (ReseñaNH)session.Load (typeof(ReseñaNH), reseña.Id);

                reseñaNH.TextoOpinion = reseña.TextoOpinion;


                reseñaNH.Valoracion = reseña.Valoracion;





                reseñaNH.Fecha = reseña.Fecha;

                session.Update (reseñaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearReseña (ReseñaEN reseña)
{
        ReseñaNH reseñaNH = new ReseñaNH (reseña);

        try
        {
                SessionInitializeTransaction ();
                if (reseña.LectorValorador != null) {
                        // Argumento OID y no colección.
                        reseñaNH
                        .LectorValorador = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN), reseña.LectorValorador.Id);

                        reseñaNH.LectorValorador.ReseñaPublicada
                        .Add (reseñaNH);
                }
                if (reseña.LibroReseñado != null) {
                        // Argumento OID y no colección.
                        reseñaNH
                        .LibroReseñado = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN), reseña.LibroReseñado.Id);

                        reseñaNH.LibroReseñado.Reseña
                        .Add (reseñaNH);
                }
                if (reseña.NotificacionReseña != null) {
                        // Argumento OID y no colección.
                        reseñaNH
                        .NotificacionReseña = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN), reseña.NotificacionReseña.Id);

                        reseñaNH.NotificacionReseña.ReseñaNotificada
                                = reseñaNH;
                }

                session.Save (reseñaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return reseñaNH.Id;
}

public void ModificarReseña (ReseñaEN reseña)
{
        try
        {
                SessionInitializeTransaction ();
                ReseñaNH reseñaNH = (ReseñaNH)session.Load (typeof(ReseñaNH), reseña.Id);

                reseñaNH.TextoOpinion = reseña.TextoOpinion;


                reseñaNH.Valoracion = reseña.Valoracion;


                reseñaNH.Fecha = reseña.Fecha;

                session.Update (reseñaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarReseña (int id
                            )
{
        try
        {
                SessionInitializeTransaction ();
                ReseñaNH reseñaNH = (ReseñaNH)session.Load (typeof(ReseñaNH), id);
                session.Delete (reseñaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameReseñaPorOID
//Con e: ReseñaEN
public ReseñaEN DameReseñaPorOID (int id
                                  )
{
        ReseñaEN reseñaEN = null;

        try
        {
                SessionInitializeTransaction ();
                reseñaEN = (ReseñaEN)session.Get (typeof(ReseñaNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return reseñaEN;
}

public System.Collections.Generic.IList<ReseñaEN> DameTodosReseñas (int first, int size)
{
        System.Collections.Generic.IList<ReseñaEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(ReseñaNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<ReseñaEN>();
                else
                        result = session.CreateCriteria (typeof(ReseñaNH)).List<ReseñaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> DameReseñasOrdenadasPorValoracionDesc (int first, int size)
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM ReseñaNH self where FROM ReseñaNH reseña order by reseña.Valoracion desc";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("ReseñaNHdameReseñasOrdenadasPorValoracionDescHQL");

                if (size > 0) {
                        query.SetFirstResult (first).SetMaxResults (size);
                }
                else{
                        query.SetFirstResult (first);
                }

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> DameReseñasOrdenadasPorValoracionAsc (int first, int size)
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM ReseñaNH self where FROM ReseñaNH reseña order by reseña.Valoracion asc";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("ReseñaNHdameReseñasOrdenadasPorValoracionAscHQL");

                if (size > 0) {
                        query.SetFirstResult (first).SetMaxResults (size);
                }
                else{
                        query.SetFirstResult (first);
                }

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ReseñaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
