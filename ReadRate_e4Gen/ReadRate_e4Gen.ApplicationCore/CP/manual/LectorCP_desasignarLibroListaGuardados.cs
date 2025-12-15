
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_desasignarLibroListaGuardados) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void DesasignarLibroListaGuardados (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroLeido_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_desasignarLibroListaGuardados) ENABLED START*/

        LectorCEN lectorCEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new  LectorCEN (CPSession.UnitRepo.LectorRepository);

                LectorEN lectorEN = lectorCEN.DameLectorPorOID (p_Lector_OID);
                List<int> librosQuitar = new List<int>();

                foreach (int libroId in p_libroLeido_OIDs) {
                        // Verificar si el libro está en la lista
                        bool estaEnLista = lectorCEN.ComprobarSiEstaEnLista (libroId, lectorEN.LibroLeido);
                        if (estaEnLista) {
                                librosQuitar.Add (libroId);
                                lectorEN.CantLibrosLeidos -= 1;
                        }
                        else{
                                throw new ModelException ("El libro con ID " + libroId + " no esta en la lista de libros guardados del lector.");
                        }
                }


                lectorCEN.get_ILectorRepository ().DesasignarLibroListaGuardados (p_Lector_OID, librosQuitar);
                lectorCEN.ModificarLector (lectorEN.Id, lectorEN.Email, lectorEN.NombreUsuario, lectorEN.FechaNacimiento, lectorEN.CiudadResidencia, lectorEN.PaisResidencia, lectorEN.Foto, lectorEN.Rol, lectorEN.Pass, lectorEN.CantLibrosCurso, lectorEN.CantLibrosLeidos, lectorEN.CantAutoresSeguidos, lectorEN.CantClubsSuscritos);


                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw ex;
        }
        finally
        {
                CPSession.SessionClose ();
        }


        /*PROTECTED REGION END*/
}
}
}
