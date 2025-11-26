
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
 * Clase Club:
 *
 */

namespace ReadRate_e4Gen.Infraestructure.Repository.ReadRate_E4
{
public partial class ClubRepository : BasicRepository, IClubRepository
{
public ClubRepository() : base ()
{
}


public ClubRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public ClubEN ReadOIDDefault (int id
                              )
{
        ClubEN clubEN = null;

        try
        {
                SessionInitializeTransaction ();
                clubEN = (ClubEN)session.Get (typeof(ClubNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return clubEN;
}

public System.Collections.Generic.IList<ClubEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<ClubEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(ClubNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<ClubEN>();
                        else
                                result = session.CreateCriteria (typeof(ClubNH)).List<ClubEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (ClubEN club)
{
        try
        {
                SessionInitializeTransaction ();
                ClubNH clubNH = (ClubNH)session.Load (typeof(ClubNH), club.Id);

                clubNH.Nombre = club.Nombre;


                clubNH.EnlaceDiscord = club.EnlaceDiscord;


                clubNH.MiembrosMax = club.MiembrosMax;


                clubNH.Foto = club.Foto;


                clubNH.Descripcion = club.Descripcion;




                clubNH.MiembrosActuales = club.MiembrosActuales;


                session.Update (clubNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearClub (ClubEN club)
{
        ClubNH clubNH = new ClubNH (club);

        try
        {
                SessionInitializeTransaction ();
                if (club.LectorPropietario != null) {
                        // p_lectorPropietario
                        club.LectorPropietario.ClubCreado.Add (clubNH);
                        session.Save (clubNH.LectorPropietario);
                }

                session.Save (clubNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return clubNH.Id;
}

public void ModificarClub (ClubEN club)
{
        try
        {
                SessionInitializeTransaction ();
                ClubNH clubNH = (ClubNH)session.Load (typeof(ClubNH), club.Id);

                clubNH.Nombre = club.Nombre;


                clubNH.EnlaceDiscord = club.EnlaceDiscord;


                clubNH.MiembrosMax = club.MiembrosMax;


                clubNH.Foto = club.Foto;


                clubNH.Descripcion = club.Descripcion;


                clubNH.MiembrosActuales = club.MiembrosActuales;

                session.Update (clubNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void EliminarClub (int id
                          )
{
        try
        {
                SessionInitializeTransaction ();
                ClubNH clubNH = (ClubNH)session.Load (typeof(ClubNH), id);
                session.Delete (clubNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DameClubPorOID
//Con e: ClubEN
public ClubEN DameClubPorOID (int id
                              )
{
        ClubEN clubEN = null;

        try
        {
                SessionInitializeTransaction ();
                clubEN = (ClubEN)session.Get (typeof(ClubNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return clubEN;
}

public System.Collections.Generic.IList<ClubEN> DameTodosClubs (int first, int size)
{
        System.Collections.Generic.IList<ClubEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(ClubNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<ClubEN>();
                else
                        result = session.CreateCriteria (typeof(ClubNH)).List<ClubEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> DameClubPorFiltros (string p_nombre, string p_descripcion, int first, int size)
{
        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM ClubNH self where FROM ClubNH club where (:p_nombre is null or club.Nombre=:p_nombre) and (:p_descripcion is null or lower(club.Descripcion) like concat('%', lower(:p_descripcion), '%'))";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("ClubNHdameClubPorFiltrosHQL");
                query.SetParameter ("p_nombre", p_nombre);
                query.SetParameter ("p_descripcion", p_descripcion);

                if (size > 0) {
                        query.SetFirstResult (first).SetMaxResults (size);
                }
                else{
                        query.SetFirstResult (first);
                }

                result = query.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.ClubEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is ReadRate_e4Gen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new ReadRate_e4Gen.ApplicationCore.Exceptions.DataLayerException ("Error in ClubRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
