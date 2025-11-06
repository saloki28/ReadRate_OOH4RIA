
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
 * Clase Evento:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class EventoRepository : BasicRepository, IEventoRepository
{
public EventoRepository() : base ()
{
}


public EventoRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public EventoEN ReadOIDDefault (int id
                                )
{
        EventoEN eventoEN = null;

        try
        {
                SessionInitializeTransaction ();
                eventoEN = (EventoEN)session.Get (typeof(EventoNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return eventoEN;
}

public System.Collections.Generic.IList<EventoEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<EventoEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(EventoNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<EventoEN>();
                        else
                                result = session.CreateCriteria (typeof(EventoNH)).List<EventoEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (EventoEN evento)
{
        try
        {
                SessionInitializeTransaction ();
                EventoNH eventoNH = (EventoNH)session.Load (typeof(EventoNH), evento.Id);

                eventoNH.Nombre = evento.Nombre;


                eventoNH.Foto = evento.Foto;


                eventoNH.Descripcion = evento.Descripcion;


                eventoNH.Fecha = evento.Fecha;


                eventoNH.Hora = evento.Hora;


                eventoNH.Ubicacion = evento.Ubicacion;


                eventoNH.AforoMax = evento.AforoMax;





                eventoNH.AforoActual = evento.AforoActual;

                session.Update (eventoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearEvento (EventoEN evento)
{
        EventoNH eventoNH = new EventoNH (evento);

        try
        {
                SessionInitializeTransaction ();
                if (evento.AdministradorEventos != null) {
                        // Argumento OID y no colección.
                        eventoNH
                        .AdministradorEventos = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN)session.Load (typeof(ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AdministradorEN), evento.AdministradorEventos.Id);

                        eventoNH.AdministradorEventos.EventoAdministrado
                        .Add (eventoNH);
                }

                session.Save (eventoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return eventoNH.Id;
}

public void ModificarEvento (EventoEN evento)
{
        try
        {
                SessionInitializeTransaction ();
                EventoNH eventoNH = (EventoNH)session.Load (typeof(EventoNH), evento.Id);

                eventoNH.Nombre = evento.Nombre;


                eventoNH.Foto = evento.Foto;


                eventoNH.Descripcion = evento.Descripcion;


                eventoNH.Fecha = evento.Fecha;


                eventoNH.Hora = evento.Hora;


                eventoNH.Ubicacion = evento.Ubicacion;


                eventoNH.AforoMax = evento.AforoMax;


                eventoNH.AforoActual = evento.AforoActual;

                session.Update (eventoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarEvento (int id
                            )
{
        try
        {
                SessionInitializeTransaction ();
                EventoNH eventoNH = (EventoNH)session.Load (typeof(EventoNH), id);
                session.Delete (eventoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameEventoPorOID
//Con e: EventoEN
public EventoEN DameEventoPorOID (int id
                                  )
{
        EventoEN eventoEN = null;

        try
        {
                SessionInitializeTransaction ();
                eventoEN = (EventoEN)session.Get (typeof(EventoNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return eventoEN;
}

public System.Collections.Generic.IList<EventoEN> DameTodosEventos (int first, int size)
{
        System.Collections.Generic.IList<EventoEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(EventoNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<EventoEN>();
                else
                        result = session.CreateCriteria (typeof(EventoNH)).List<EventoEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> DameEventosPorFecha (Nullable<DateTime> p_fecha, int first, int size)
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM EventoNH self where FROM EventoNH evento where (:p_fecha is null or evento.Fecha>=:p_fecha)";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("EventoNHdameEventosPorFechaHQL");
                query.SetParameter ("p_fecha", p_fecha);

                if (size > 0) {
                        query.SetFirstResult (first).SetMaxResults (size);
                }
                else{
                        query.SetFirstResult (first);
                }

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in EventoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
