
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_desinscribirLectorDeEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class LectorCP : GenericBasicCP
{
public void DesinscribirLectorDeEvento (int p_Lector_OID, System.Collections.Generic.IList<int> p_eventoLector_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Lector_desinscribirLectorDeEvento) ENABLED START*/

        ILectorRepository lectorRepository = null;
        IEventoRepository eventoRepository = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                lectorRepository = CPSession.UnitRepo.LectorRepository;
                eventoRepository = CPSession.UnitRepo.EventoRepository;

                //diminuir aforo actual y validar inscripción
                foreach (int eventoId in p_eventoLector_OIDs) { // Evento a desinscribir
                        EventoEN eventoEN = eventoRepository.DameEventoPorOID (eventoId);

                        if (eventoEN.AforoActual <= 0) { // No hay gente inscrita
                                throw new ModelException ("No hay usuarios inscritos en el evento: " + eventoEN.Nombre);
                        }

                        // Obtener el evento con sus lectores participantes para verificar inscripción
                        // Nota: Esto puede causar lazy loading de la colección LectorParticipante
                        bool lectorInscrito = false;

                        // Intentar buscar en la colección de participantes
                        try {
                                if (eventoEN.LectorParticipante != null && eventoEN.LectorParticipante.Count > 0) {
                                        foreach (var participante in eventoEN.LectorParticipante) {
                                                if (participante.Id == p_Lector_OID) {
                                                        lectorInscrito = true;
                                                        break;
                                                }
                                        }
                                }
                        } catch (Exception) {
                                // Si falla al cargar la colección por polimorfismo, asumir que no está inscrito
                                // (esto no debería pasar si la relación está bien configurada)
                        }

                        if (!lectorInscrito) {
                                throw new ModelException ("El usuario no está inscrito en el evento: " + eventoEN.Nombre);
                        }

                        eventoEN.AforoActual -= 1; //disminuir aforo actual
                }

                // Ejecutar la desinscripción
                lectorRepository.DesinscribirLectorDeEvento (p_Lector_OID, p_eventoLector_OIDs);

                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw;
        }
        finally
        {
                CPSession.SessionClose ();
        }


        /*PROTECTED REGION END*/
}
}
}
