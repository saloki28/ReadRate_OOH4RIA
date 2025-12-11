
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Evento_obtenerListaParticipantes) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class EventoCP : GenericBasicCP
{
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> ObtenerListaParticipantes (int p_oid)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Evento_obtenerListaParticipantes) ENABLED START*/

        EventoCEN eventoCEN = null;

        System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN>  result = null;


        try
        {
                CPSession.SessionInitializeTransaction ();

                eventoCEN = new EventoCEN (CPSession.UnitRepo.EventoRepository);
                EventoEN eventoEN = eventoCEN.DameEventoPorOID (p_oid);

                if (eventoEN == null) {
                        throw new ModelException ("El evento con ID " + p_oid + " no existe.");
                }

                // Crear una lista combinada de participantes (autores + lectores)
                var participantes = new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN>();

                // Añadir lectores participantes
                if (eventoEN.LectorParticipante != null) {
                        foreach (var lector in eventoEN.LectorParticipante) {
                                participantes.Add (lector);
                        }
                }

                // Añadir autores participantes
                if (eventoEN.AutorParticipante != null) {
                        foreach (var autor in eventoEN.AutorParticipante) {
                                participantes.Add (autor);
                        }
                }

                return participantes;



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
        return result;


        /*PROTECTED REGION END*/
}
}
}
