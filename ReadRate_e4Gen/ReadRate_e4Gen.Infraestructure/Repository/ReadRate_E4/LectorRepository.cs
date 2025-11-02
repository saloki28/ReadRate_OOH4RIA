
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
 * Clase Lector:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class LectorRepository : BasicRepository, ILectorRepository
{
public LectorRepository() : base ()
{
}


public LectorRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public LectorEN ReadOIDDefault (int id
                                )
{
        LectorEN lectorEN = null;

        try
        {
                SessionInitializeTransaction ();
                lectorEN = (LectorEN)session.Get (typeof(LectorNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return lectorEN;
}

public System.Collections.Generic.IList<LectorEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<LectorEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(LectorNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<LectorEN>();
                        else
                                result = session.CreateCriteria (typeof(LectorNH)).List<LectorEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (LectorEN lector)
{
        try
        {
                SessionInitializeTransaction ();
                LectorNH lectorNH = (LectorNH)session.Load (typeof(LectorNH), lector.Id);

                lectorNH.CantLibrosCurso = lector.CantLibrosCurso;


                lectorNH.CantLibrosLeidos = lector.CantLibrosLeidos;


                lectorNH.CantAutoresSeguidos = lector.CantAutoresSeguidos;


                lectorNH.CantClubsSuscritos = lector.CantClubsSuscritos;





                session.Update (lectorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearLector (LectorEN lector)
{
        LectorNH lectorNH = new LectorNH (lector);

        try
        {
                SessionInitializeTransaction ();

                session.Save (lectorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return lectorNH.Id;
}

public void ModificarLector (LectorEN lector)
{
        try
        {
                SessionInitializeTransaction ();
                LectorNH lectorNH = (LectorNH)session.Load (typeof(LectorNH), lector.Id);

                lectorNH.Email = lector.Email;


                lectorNH.NombreUsuario = lector.NombreUsuario;


                lectorNH.FechaNacimiento = lector.FechaNacimiento;


                lectorNH.CiudadResidencia = lector.CiudadResidencia;


                lectorNH.PaisResidencia = lector.PaisResidencia;


                lectorNH.Foto = lector.Foto;


                lectorNH.Rol = lector.Rol;


                lectorNH.Pass = lector.Pass;


                lectorNH.CantLibrosCurso = lector.CantLibrosCurso;


                lectorNH.CantLibrosLeidos = lector.CantLibrosLeidos;


                lectorNH.CantAutoresSeguidos = lector.CantAutoresSeguidos;


                lectorNH.CantClubsSuscritos = lector.CantClubsSuscritos;

                session.Update (lectorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarLector (int id
                            )
{
        try
        {
                SessionInitializeTransaction ();
                LectorNH lectorNH = (LectorNH)session.Load (typeof(LectorNH), id);
                session.Delete (lectorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void SeguirAutor (int p_Lector_OID, System.Collections.Generic.IList<int> p_autorSeguido_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorEN = null;
        try
        {
                SessionInitializeTransaction ();
                lectorEN = (LectorEN)session.Load (typeof(LectorNH), p_Lector_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorSeguidoENAux = null;
                if (lectorEN.AutorSeguido == null) {
                        lectorEN.AutorSeguido = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN>();
                }

                foreach (int item in p_autorSeguido_OIDs) {
                        autorSeguidoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN ();
                        autorSeguidoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.AutorNH), item);
                        autorSeguidoENAux.LectorSeguidor.Add (lectorEN);

                        lectorEN.AutorSeguido.Add (autorSeguidoENAux);
                }


                session.Update (lectorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DejarDeSeguirAutor (int p_Lector_OID, System.Collections.Generic.IList<int> p_autorSeguido_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorEN = null;
                lectorEN = (LectorEN)session.Load (typeof(LectorNH), p_Lector_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN autorSeguidoENAux = null;
                if (lectorEN.AutorSeguido != null) {
                        foreach (int item in p_autorSeguido_OIDs) {
                                autorSeguidoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.AutorEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.AutorNH), item);
                                if (lectorEN.AutorSeguido.Contains (autorSeguidoENAux) == true) {
                                        lectorEN.AutorSeguido.Remove (autorSeguidoENAux);
                                        autorSeguidoENAux.LectorSeguidor.Remove (lectorEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_autorSeguido_OIDs you are trying to unrelationer, doesn't exist in LectorEN");
                        }
                }

                session.Update (lectorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
//Sin e: DameLectorPorOID
//Con e: LectorEN
public LectorEN DameLectorPorOID (int id
                                  )
{
        LectorEN lectorEN = null;

        try
        {
                SessionInitializeTransaction ();
                lectorEN = (LectorEN)session.Get (typeof(LectorNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return lectorEN;
}

public System.Collections.Generic.IList<LectorEN> DameTodosLectores (int first, int size)
{
        System.Collections.Generic.IList<LectorEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(LectorNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<LectorEN>();
                else
                        result = session.CreateCriteria (typeof(LectorNH)).List<LectorEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public void AsignarLibroListaGuardados (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroLeido_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorEN = null;
        try
        {
                SessionInitializeTransaction ();
                lectorEN = (LectorEN)session.Load (typeof(LectorNH), p_Lector_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroLeidoENAux = null;
                if (lectorEN.LibroLeido == null) {
                        lectorEN.LibroLeido = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
                }

                foreach (int item in p_libroLeido_OIDs) {
                        libroLeidoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN ();
                        libroLeidoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.LibroNH), item);
                        libroLeidoENAux.LectorGuardando.Add (lectorEN);

                        lectorEN.LibroLeido.Add (libroLeidoENAux);
                }


                session.Update (lectorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DesasignarLibroListaGuardados (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroLeido_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorEN = null;
                lectorEN = (LectorEN)session.Load (typeof(LectorNH), p_Lector_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroLeidoENAux = null;
                if (lectorEN.LibroLeido != null) {
                        foreach (int item in p_libroLeido_OIDs) {
                                libroLeidoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.LibroNH), item);
                                if (lectorEN.LibroLeido.Contains (libroLeidoENAux) == true) {
                                        lectorEN.LibroLeido.Remove (libroLeidoENAux);
                                        libroLeidoENAux.LectorGuardando.Remove (lectorEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_libroLeido_OIDs you are trying to unrelationer, doesn't exist in LectorEN");
                        }
                }

                session.Update (lectorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void AsignarLibroListaEnCurso (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroEnCurso_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorEN = null;
        try
        {
                SessionInitializeTransaction ();
                lectorEN = (LectorEN)session.Load (typeof(LectorNH), p_Lector_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroEnCursoENAux = null;
                if (lectorEN.LibroEnCurso == null) {
                        lectorEN.LibroEnCurso = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN>();
                }

                foreach (int item in p_libroEnCurso_OIDs) {
                        libroEnCursoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN ();
                        libroEnCursoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.LibroNH), item);
                        libroEnCursoENAux.LectorLeyendo.Add (lectorEN);

                        lectorEN.LibroEnCurso.Add (libroEnCursoENAux);
                }


                session.Update (lectorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DesasignarLibroListaEnCurso (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroEnCurso_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LectorEN lectorEN = null;
                lectorEN = (LectorEN)session.Load (typeof(LectorNH), p_Lector_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN libroEnCursoENAux = null;
                if (lectorEN.LibroEnCurso != null) {
                        foreach (int item in p_libroEnCurso_OIDs) {
                                libroEnCursoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.LibroEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.LibroNH), item);
                                if (lectorEN.LibroEnCurso.Contains (libroEnCursoENAux) == true) {
                                        lectorEN.LibroEnCurso.Remove (libroEnCursoENAux);
                                        libroEnCursoENAux.LectorLeyendo.Remove (lectorEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_libroEnCurso_OIDs you are trying to unrelationer, doesn't exist in LectorEN");
                        }
                }

                session.Update (lectorEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in LectorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
}
}
