
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_desinscribirAutorDeEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class AutorCP : GenericBasicCP
{
public void DesinscribirAutorDeEvento (int p_Autor_OID, System.Collections.Generic.IList<int> p_eventoAutor_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_desinscribirAutorDeEvento) ENABLED START*/

        IAutorRepository autorRepository = null;
        IEventoRepository eventoRepository = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                autorRepository = CPSession.UnitRepo.AutorRepository;
                eventoRepository = CPSession.UnitRepo.EventoRepository;

                // Disminuir aforo actual y validar inscripción
                foreach (int eventoId in p_eventoAutor_OIDs) { // Evento a desinscribir
                        EventoEN eventoEN = eventoRepository.DameEventoPorOID (eventoId);

                        if (eventoEN.AforoActual <= 0) { // No hay ausuarios inscritos
                                throw new ModelException ("No hay autores inscritos en el evento: " + eventoEN.Nombre);
                        }

                        // Obtener el evento con sus autores participantes para verificar inscripción
                        // Nota: Esto puede causar lazy loading de la colección AutorParticipante
                        bool autorInscrito = false;

                        // Intentar buscar en la colección de participantes
                        try {
                                if (eventoEN.AutorParticipante != null && eventoEN.AutorParticipante.Count > 0) {
                                        foreach (var participante in eventoEN.AutorParticipante) {
                                                if (participante.Id == p_Autor_OID) {
                                                        autorInscrito = true;
                                                        break;
                                                }
                                        }
                                }
                        } catch (Exception) {
                                // Si falla al cargar la colección por polimorfismo, asumir que no está inscrito
                                // (esto no debería pasar si la relación está bien configurada)
                        }

                        if (!autorInscrito) {
                                throw new ModelException ("El autor no está inscrito en el evento: " + eventoEN.Nombre);
                        }

                        eventoEN.AforoActual -= 1; //disminuir aforo actual
                }

                // Ejecutar la desinscripción
                autorRepository.DesinscribirAutorDeEvento (p_Autor_OID, p_eventoAutor_OIDs);

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
