
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
 * Clase Notificacion:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class NotificacionRepository : BasicRepository, INotificacionRepository
{
public NotificacionRepository() : base ()
{
}


public NotificacionRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public NotificacionEN ReadOIDDefault (int id
                                      )
{
        NotificacionEN notificacionEN = null;

        try
        {
                SessionInitializeTransaction ();
                notificacionEN = (NotificacionEN)session.Get (typeof(NotificacionNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return notificacionEN;
}

public System.Collections.Generic.IList<NotificacionEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<NotificacionEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(NotificacionNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<NotificacionEN>();
                        else
                                result = session.CreateCriteria (typeof(NotificacionNH)).List<NotificacionEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (NotificacionEN notificacion)
{
        try
        {
                SessionInitializeTransaction ();
                NotificacionNH notificacionNH = (NotificacionNH)session.Load (typeof(NotificacionNH), notificacion.Id);

                notificacionNH.Fecha = notificacion.Fecha;


                notificacionNH.Concepto = notificacion.Concepto;







                session.Update (notificacionNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearNotificacion (NotificacionEN notificacion)
{
        NotificacionNH notificacionNH = new NotificacionNH (notificacion);

        try
        {
                SessionInitializeTransaction ();
                if (notificacion.NoticiaNotificada != null) {
                        // Argumento OID y no colección.
                        notificacionNH
                        .NoticiaNotificada = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NoticiaEN), notificacion.NoticiaNotificada.Id);

                        notificacionNH.NoticiaNotificada.NotificacionNoticia
                        .Add (notificacionNH);
                }
                if (notificacion.EventoNotificado != null) {
                        // Argumento OID y no colección.
                        notificacionNH
                        .EventoNotificado = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN), notificacion.EventoNotificado.Id);

                        notificacionNH.EventoNotificado.NotificacionEvento
                        .Add (notificacionNH);
                }
                if (notificacion.ClubNotificado != null) {
                        // Argumento OID y no colección.
                        notificacionNH
                        .ClubNotificado = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN), notificacion.ClubNotificado.Id);

                        notificacionNH.ClubNotificado.NotificacionClub
                        .Add (notificacionNH);
                }
                if (notificacion.AutorAvisado != null) {
                        // Argumento OID y no colección.
                        notificacionNH
                        .AutorAvisado = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN), notificacion.AutorAvisado.Id);

                        notificacionNH.AutorAvisado.AvisoReseña
                        .Add (notificacionNH);
                }
                if (notificacion.ReseñaNotificada != null) {
                        // Argumento OID y no colección.
                        notificacionNH
                        .ReseñaNotificada = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ReseñaEN), notificacion.ReseñaNotificada.Id);

                        notificacionNH.ReseñaNotificada.NotificacionReseña
                        .Add (notificacionNH);
                }

                session.Save (notificacionNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return notificacionNH.Id;
}

public void ModificarNotificacion (NotificacionEN notificacion)
{
        try
        {
                SessionInitializeTransaction ();
                NotificacionNH notificacionNH = (NotificacionNH)session.Load (typeof(NotificacionNH), notificacion.Id);

                notificacionNH.Fecha = notificacion.Fecha;


                notificacionNH.Concepto = notificacion.Concepto;

                session.Update (notificacionNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarNotificacion (int id
                                  )
{
        try
        {
                SessionInitializeTransaction ();
                NotificacionNH notificacionNH = (NotificacionNH)session.Load (typeof(NotificacionNH), id);
                session.Delete (notificacionNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameNotificacionPorOID
//Con e: NotificacionEN
public NotificacionEN DameNotificacionPorOID (int id
                                              )
{
        NotificacionEN notificacionEN = null;

        try
        {
                SessionInitializeTransaction ();
                notificacionEN = (NotificacionEN)session.Get (typeof(NotificacionNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return notificacionEN;
}

public System.Collections.Generic.IList<NotificacionEN> DameTodosNotificaciones (int first, int size)
{
        System.Collections.Generic.IList<NotificacionEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(NotificacionNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<NotificacionEN>();
                else
                        result = session.CreateCriteria (typeof(NotificacionNH)).List<NotificacionEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public void VincularNotificacionAUsuario (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_usuarioNotificado_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionEN = null;
        try
        {
                SessionInitializeTransaction ();
                notificacionEN = (NotificacionEN)session.Load (typeof(NotificacionNH), p_Notificacion_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioNotificadoENAux = null;
                if (notificacionEN.UsuarioNotificado == null) {
                        notificacionEN.UsuarioNotificado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN>();
                }

                foreach (int item in p_usuarioNotificado_OIDs) {
                        usuarioNotificadoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN ();
                        usuarioNotificadoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.UsuarioNH), item);
                        usuarioNotificadoENAux.Notificacion.Add (notificacionEN);

                        notificacionEN.UsuarioNotificado.Add (usuarioNotificadoENAux);
                }


                session.Update (notificacionEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DesvincularNotificacionAUsuario (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_usuarioNotificado_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionEN = null;
                notificacionEN = (NotificacionEN)session.Load (typeof(NotificacionNH), p_Notificacion_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioNotificadoENAux = null;
                if (notificacionEN.UsuarioNotificado != null) {
                        foreach (int item in p_usuarioNotificado_OIDs) {
                                usuarioNotificadoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.UsuarioNH), item);
                                if (notificacionEN.UsuarioNotificado.Contains (usuarioNotificadoENAux) == true) {
                                        notificacionEN.UsuarioNotificado.Remove (usuarioNotificadoENAux);
                                        usuarioNotificadoENAux.Notificacion.Remove (notificacionEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_usuarioNotificado_OIDs you are trying to unrelationer, doesn't exist in NotificacionEN");
                        }
                }

                session.Update (notificacionEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in NotificacionRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
}
}
