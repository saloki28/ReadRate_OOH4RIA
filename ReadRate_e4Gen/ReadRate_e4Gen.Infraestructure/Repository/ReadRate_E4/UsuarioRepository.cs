
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
 * Clase Usuario:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class UsuarioRepository : BasicRepository, IUsuarioRepository
{
public UsuarioRepository() : base ()
{
}


public UsuarioRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public UsuarioEN ReadOIDDefault (int id
                                 )
{
        UsuarioEN usuarioEN = null;

        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Get (typeof(UsuarioNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return usuarioEN;
}

public System.Collections.Generic.IList<UsuarioEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<UsuarioEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(UsuarioNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<UsuarioEN>();
                        else
                                result = session.CreateCriteria (typeof(UsuarioNH)).List<UsuarioEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (UsuarioEN usuario)
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioNH usuarioNH = (UsuarioNH)session.Load (typeof(UsuarioNH), usuario.Id);

                usuarioNH.Email = usuario.Email;


                usuarioNH.NombreUsuario = usuario.NombreUsuario;


                usuarioNH.FechaNacimiento = usuario.FechaNacimiento;


                usuarioNH.CiudadResidencia = usuario.CiudadResidencia;


                usuarioNH.PaisResidencia = usuario.PaisResidencia;


                usuarioNH.Foto = usuario.Foto;


                usuarioNH.Rol = usuario.Rol;






                usuarioNH.Pass = usuario.Pass;

                session.Update (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearUsuario (UsuarioEN usuario)
{
        UsuarioNH usuarioNH = new UsuarioNH (usuario);

        try
        {
                SessionInitializeTransaction ();

                session.Save (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return usuarioNH.Id;
}

public void ModificarUsuario (UsuarioEN usuario)
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioNH usuarioNH = (UsuarioNH)session.Load (typeof(UsuarioNH), usuario.Id);

                usuarioNH.Email = usuario.Email;


                usuarioNH.NombreUsuario = usuario.NombreUsuario;


                usuarioNH.FechaNacimiento = usuario.FechaNacimiento;


                usuarioNH.CiudadResidencia = usuario.CiudadResidencia;


                usuarioNH.PaisResidencia = usuario.PaisResidencia;


                usuarioNH.Foto = usuario.Foto;


                usuarioNH.Rol = usuario.Rol;


                usuarioNH.Pass = usuario.Pass;

                session.Update (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarUsuario (int id
                             )
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioNH usuarioNH = (UsuarioNH)session.Load (typeof(UsuarioNH), id);
                session.Delete (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameUsuarioPorOID
//Con e: UsuarioEN
public UsuarioEN DameUsuarioPorOID (int id
                                    )
{
        UsuarioEN usuarioEN = null;

        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Get (typeof(UsuarioNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return usuarioEN;
}

public System.Collections.Generic.IList<UsuarioEN> DameTodosUsuarios (int first, int size)
{
        System.Collections.Generic.IList<UsuarioEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(UsuarioNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<UsuarioEN>();
                else
                        result = session.CreateCriteria (typeof(UsuarioNH)).List<UsuarioEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public void SuscribirAClub (int p_Usuario_OID, System.Collections.Generic.IList<int> p_clubSuscrito_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioEN = null;
        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubSuscritoENAux = null;
                if (usuarioEN.ClubSuscrito == null) {
                        usuarioEN.ClubSuscrito = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN>();
                }

                foreach (int item in p_clubSuscrito_OIDs) {
                        clubSuscritoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN ();
                        clubSuscritoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.ClubNH), item);
                        clubSuscritoENAux.UsuarioMiembro.Add (usuarioEN);

                        usuarioEN.ClubSuscrito.Add (clubSuscritoENAux);
                }


                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DesuscribirDeClub (int p_Usuario_OID, System.Collections.Generic.IList<int> p_clubSuscrito_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioEN = null;
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN clubSuscritoENAux = null;
                if (usuarioEN.ClubSuscrito != null) {
                        foreach (int item in p_clubSuscrito_OIDs) {
                                clubSuscritoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.ClubNH), item);
                                if (usuarioEN.ClubSuscrito.Contains (clubSuscritoENAux) == true) {
                                        usuarioEN.ClubSuscrito.Remove (clubSuscritoENAux);
                                        clubSuscritoENAux.UsuarioMiembro.Remove (usuarioEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_clubSuscrito_OIDs you are trying to unrelationer, doesn't exist in UsuarioEN");
                        }
                }

                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void InscribirAEvento (int p_Usuario_OID, System.Collections.Generic.IList<int> p_evento_OIDs)
{
        ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioEN = null;
        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoENAux = null;
                if (usuarioEN.Evento == null) {
                        usuarioEN.Evento = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN>();
                }

                foreach (int item in p_evento_OIDs) {
                        eventoENAux = new ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN ();
                        eventoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.EventoNH), item);
                        eventoENAux.UsuarioParticipante.Add (usuarioEN);

                        usuarioEN.Evento.Add (eventoENAux);
                }


                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DesinscribirDeEvento (int p_Usuario_OID, System.Collections.Generic.IList<int> p_evento_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN usuarioEN = null;
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);

                ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN eventoENAux = null;
                if (usuarioEN.Evento != null) {
                        foreach (int item in p_evento_OIDs) {
                                eventoENAux = (ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.EventoEN)session.Load (typeof(ReadRate_e4Gen.Infraestructure.EN.ReadRate_E4.EventoNH), item);
                                if (usuarioEN.Evento.Contains (eventoENAux) == true) {
                                        usuarioEN.Evento.Remove (eventoENAux);
                                        eventoENAux.UsuarioParticipante.Remove (usuarioEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_evento_OIDs you are trying to unrelationer, doesn't exist in UsuarioEN");
                        }
                }

                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> DameUsuarioPorEmail (string p_email)
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM UsuarioNH self where FROM UsuarioNH usuario where usuario.Email = :p_email";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("UsuarioNHdameUsuarioPorEmailHQL");
                query.SetParameter ("p_email", p_email);

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
