
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_seguirAutor) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void SeguirAutor (int p_Lector_OID, System.Collections.Generic.IList<int> p_autorSeguido_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_seguirAutor) ENABLED START*/

        try
        {
                CPSession.SessionInitializeTransaction ();

                // Obtener repositorios
                ILectorRepository lectorRepository = CPSession.UnitRepo.LectorRepository;
                IAutorRepository autorRepository = CPSession.UnitRepo.AutorRepository;

                // Obtener el lector
                LectorEN lectorEN = lectorRepository.DameLectorPorOID (p_Lector_OID);

                if (lectorEN == null) {
                        throw new ModelException ("El lector no existe.");
                }

                // Procesar cada autor en la lista
                foreach (int autorId in p_autorSeguido_OIDs) {
                        // Obtener el autor
                        AutorEN autorEN = autorRepository.DameAutorPorOID (autorId);

                        if (autorEN == null) {
                                throw new ModelException ("El autor con ID " + autorId + " no existe.");
                        }

                        // Verificar si el lector ya sigue al autor
                        if (lectorEN.AutorSeguido == null) {
                                lectorEN.AutorSeguido = new List<AutorEN>();
                        }

                        bool yaSiguiendoAutor = false;
                        foreach (AutorEN autor in lectorEN.AutorSeguido) {
                                if (autor.Id == autorId) {
                                        yaSiguiendoAutor = true;
                                        break;
                                }
                        }

                        if (yaSiguiendoAutor) {
                                throw new ModelException ("El lector ya está siguiendo al autor con ID " + autorId + ".");
                        }

                        // Agregar el autor a la lista de autores seguidos del lector
                        lectorEN.AutorSeguido.Add (autorEN);
                        lectorEN.CantAutoresSeguidos++;

                        // Agregar el lector a la lista de seguidores del autor
                        if (autorEN.LectorSeguidor == null) {
                                autorEN.LectorSeguidor = new List<LectorEN>();
                        }
                        autorEN.LectorSeguidor.Add (lectorEN);
                        autorEN.NumeroSeguidores++;

                        // Actualizar el autor
                        autorRepository.ModificarAutor (autorEN);
                }

                // Actualizar el lector
                lectorRepository.ModificarLector (lectorEN);

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
