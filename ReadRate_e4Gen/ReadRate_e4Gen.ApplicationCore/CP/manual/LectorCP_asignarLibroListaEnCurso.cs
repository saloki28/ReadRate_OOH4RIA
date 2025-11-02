
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_asignarLibroListaEnCurso) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void AsignarLibroListaEnCurso (int p_Lector_OID, System.Collections.Generic.IList<int> p_libroEnCurso_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_asignarLibroListaEnCurso) ENABLED START*/

        LectorCEN lectorCEN = null;

            try
            {
                CPSession.SessionInitializeTransaction ();
                lectorCEN = new  LectorCEN (CPSession.UnitRepo.LectorRepository);

                LectorEN lectorEN = lectorCEN.DameLectorPorOID (p_Lector_OID);
                List<int> librosAgregar = new List<int>();

                foreach (int libroId in p_libroEnCurso_OIDs)
                {
                        // Verificar si el libro ya está en la lista
                        bool yaEnLista = lectorCEN.ComprobarSiEstaEnLista (libroId, lectorEN.LibroEnCurso);
                        if (!yaEnLista)
                        {
                            librosAgregar.Add (libroId);
                        }
                    else
                    {
                        throw new ModelException ("El libro con ID " + libroId + " ya está en la lista de libros en curso del lector.");
                    }
                }


                lectorCEN.get_ILectorRepository ().AsignarLibroListaEnCurso (p_Lector_OID, librosAgregar);



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
