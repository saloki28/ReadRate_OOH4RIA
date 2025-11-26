
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
 * Clase Autor:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class AutorRepository : BasicRepository, IAutorRepository
{
public AutorRepository() : base ()
{
}


public AutorRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public AutorEN ReadOIDDefault (int id
                               )
{
        AutorEN autorEN = null;

        try
        {
                SessionInitializeTransaction ();
                autorEN = (AutorEN)session.Get (typeof(AutorNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return autorEN;
}

public System.Collections.Generic.IList<AutorEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<AutorEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(AutorNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<AutorEN>();
                        else
                                result = session.CreateCriteria (typeof(AutorNH)).List<AutorEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (AutorEN autor)
{
        try
        {
                SessionInitializeTransaction ();
                AutorNH autorNH = (AutorNH)session.Load (typeof(AutorNH), autor.Id);

                autorNH.NumeroSeguidores = autor.NumeroSeguidores;


                autorNH.CantidadLibrosPublicados = autor.CantidadLibrosPublicados;


                autorNH.ValoracionMedia = autor.ValoracionMedia;





                session.Update (autorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearAutor (AutorEN autor)
{
        AutorNH autorNH = new AutorNH (autor);

        try
        {
                SessionInitializeTransaction ();

                session.Save (autorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return autorNH.Id;
}

public void ModificarAutor (AutorEN autor)
{
        try
        {
                SessionInitializeTransaction ();
                AutorNH autorNH = (AutorNH)session.Load (typeof(AutorNH), autor.Id);

                autorNH.Email = autor.Email;


                autorNH.NombreUsuario = autor.NombreUsuario;


                autorNH.FechaNacimiento = autor.FechaNacimiento;


                autorNH.CiudadResidencia = autor.CiudadResidencia;


                autorNH.PaisResidencia = autor.PaisResidencia;


                autorNH.Foto = autor.Foto;


                autorNH.Rol = autor.Rol;


                autorNH.Pass = autor.Pass;


                autorNH.NumModificaciones = autor.NumModificaciones;


                autorNH.NumeroSeguidores = autor.NumeroSeguidores;


                autorNH.CantidadLibrosPublicados = autor.CantidadLibrosPublicados;


                autorNH.ValoracionMedia = autor.ValoracionMedia;

                session.Update (autorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarAutor (int id
                           )
{
        try
        {
                SessionInitializeTransaction ();
                AutorNH autorNH = (AutorNH)session.Load (typeof(AutorNH), id);
                session.Delete (autorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameAutorPorOID
//Con e: AutorEN
public AutorEN DameAutorPorOID (int id
                                )
{
        AutorEN autorEN = null;

        try
        {
                SessionInitializeTransaction ();
                autorEN = (AutorEN)session.Get (typeof(AutorNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return autorEN;
}

public System.Collections.Generic.IList<AutorEN> DameTodosAutores (int first, int size)
{
        System.Collections.Generic.IList<AutorEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(AutorNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<AutorEN>();
                else
                        result = session.CreateCriteria (typeof(AutorNH)).List<AutorEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public void InscribirAutorAEvento (int p_Autor_OID, System.Collections.Generic.IList<int> p_eventoAutor_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorEN = null;
        try
        {
                SessionInitializeTransaction ();
                autorEN = (AutorEN)session.Load (typeof(AutorNH), p_Autor_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoAutorENAux = null;
                if (autorEN.EventoAutor == null) {
                        autorEN.EventoAutor = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN>();
                }

                foreach (int item in p_eventoAutor_OIDs) {
                        eventoAutorENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN ();
                        eventoAutorENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.EventoNH), item);
                        eventoAutorENAux.AutorParticipante.Add (autorEN);

                        autorEN.EventoAutor.Add (eventoAutorENAux);
                }


                session.Update (autorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DesinscribirAutorDeEvento (int p_Autor_OID, System.Collections.Generic.IList<int> p_eventoAutor_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorEN = null;
                autorEN = (AutorEN)session.Load (typeof(AutorNH), p_Autor_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoAutorENAux = null;
                if (autorEN.EventoAutor != null) {
                        foreach (int item in p_eventoAutor_OIDs) {
                                eventoAutorENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.EventoNH), item);
                                if (autorEN.EventoAutor.Contains (eventoAutorENAux) == true) {
                                        autorEN.EventoAutor.Remove (eventoAutorENAux);
                                        eventoAutorENAux.AutorParticipante.Remove (autorEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_eventoAutor_OIDs you are trying to unrelationer, doesn't exist in AutorEN");
                        }
                }

                session.Update (autorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in AutorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
}
}
