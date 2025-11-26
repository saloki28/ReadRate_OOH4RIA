
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








                notificacionNH.TituloResumen = notificacion.TituloResumen;


                notificacionNH.TextoCuerpo = notificacion.TextoCuerpo;

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


                notificacionNH.TituloResumen = notificacion.TituloResumen;


                notificacionNH.TextoCuerpo = notificacion.TextoCuerpo;

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

public void VincularNotificacionAutor (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_autorNotificado_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionEN = null;
        try
        {
                SessionInitializeTransaction ();
                notificacionEN = (NotificacionEN)session.Load (typeof(NotificacionNH), p_Notificacion_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorNotificadoENAux = null;
                if (notificacionEN.AutorNotificado == null) {
                        notificacionEN.AutorNotificado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN>();
                }

                foreach (int item in p_autorNotificado_OIDs) {
                        autorNotificadoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN ();
                        autorNotificadoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.AutorNH), item);
                        autorNotificadoENAux.NotificacionAutor.Add (notificacionEN);

                        notificacionEN.AutorNotificado.Add (autorNotificadoENAux);
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

public void DesvincularNotificacionAutor (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_autorNotificado_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionEN = null;
                notificacionEN = (NotificacionEN)session.Load (typeof(NotificacionNH), p_Notificacion_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorNotificadoENAux = null;
                if (notificacionEN.AutorNotificado != null) {
                        foreach (int item in p_autorNotificado_OIDs) {
                                autorNotificadoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.AutorNH), item);
                                if (notificacionEN.AutorNotificado.Contains (autorNotificadoENAux) == true) {
                                        notificacionEN.AutorNotificado.Remove (autorNotificadoENAux);
                                        autorNotificadoENAux.NotificacionAutor.Remove (notificacionEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_autorNotificado_OIDs you are trying to unrelationer, doesn't exist in NotificacionEN");
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
public void VincularNotificacionLector (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_lectorNotificado_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionEN = null;
        try
        {
                SessionInitializeTransaction ();
                notificacionEN = (NotificacionEN)session.Load (typeof(NotificacionNH), p_Notificacion_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorNotificadoENAux = null;
                if (notificacionEN.LectorNotificado == null) {
                        notificacionEN.LectorNotificado = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN>();
                }

                foreach (int item in p_lectorNotificado_OIDs) {
                        lectorNotificadoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN ();
                        lectorNotificadoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.LectorNH), item);
                        lectorNotificadoENAux.NotificacionLector.Add (notificacionEN);

                        notificacionEN.LectorNotificado.Add (lectorNotificadoENAux);
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

public void DesvincularNotificacionLector (int p_Notificacion_OID, System.Collections.Generic.IList<int> p_lectorNotificado_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.NotificacionEN notificacionEN = null;
                notificacionEN = (NotificacionEN)session.Load (typeof(NotificacionNH), p_Notificacion_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorNotificadoENAux = null;
                if (notificacionEN.LectorNotificado != null) {
                        foreach (int item in p_lectorNotificado_OIDs) {
                                lectorNotificadoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.LectorNH), item);
                                if (notificacionEN.LectorNotificado.Contains (lectorNotificadoENAux) == true) {
                                        notificacionEN.LectorNotificado.Remove (lectorNotificadoENAux);
                                        lectorNotificadoENAux.NotificacionLector.Remove (notificacionEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_lectorNotificado_OIDs you are trying to unrelationer, doesn't exist in NotificacionEN");
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
