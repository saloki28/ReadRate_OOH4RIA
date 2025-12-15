
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_inscribirAutorAEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class AutorCP : GenericBasicCP
{
public void InscribirAutorAEvento (int p_Autor_OID, System.Collections.Generic.IList<int> p_eventoAutor_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Autor_inscribirAutorAEvento) ENABLED START*/

        IAutorRepository autorRepository = null;
        IEventoRepository eventoRepository = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                autorRepository = CPSession.UnitRepo.AutorRepository;
                eventoRepository = CPSession.UnitRepo.EventoRepository;

                // Comprobar aforo maximo, verificar que no está inscrito y aumentar aforo actual
                foreach (int eventoId in p_eventoAutor_OIDs) { // Evento a inscribir
                        EventoEN eventoEN = eventoRepository.DameEventoPorOID (eventoId);

                        if (eventoEN.AforoActual >= eventoEN.AforoMax) { // Aforo maximo alcanzado
                                throw new ModelException ("No se puede inscribir al evento " + eventoEN.Nombre + " porque ha alcanzado su aforo máximo.");
                        }

                        // Verificar si el usuario ya está inscrito en el evento
                        bool usuarioYaInscrito = false;

                        try {
                                if (eventoEN.AutorParticipante != null && eventoEN.AutorParticipante.Count > 0) {
                                        foreach (var participante in eventoEN.AutorParticipante) {
                                                if (participante.Id == p_Autor_OID) {
                                                        usuarioYaInscrito = true;
                                                        break;
                                                }
                                        }
                                }
                        } catch (Exception) {
                                // Si falla al cargar la colección, asumir que no está inscrito y continuar
                        }

                        if (usuarioYaInscrito) {
                                throw new ModelException ("No se puede inscribir al evento " + eventoEN.Nombre + " porque el usuario ya está inscrito en el evento.");
                        }

                        eventoEN.AforoActual += 1; // Aumentar aforo actual
                }

                // Ejecutar la inscripción
                autorRepository.InscribirAutorAEvento (p_Autor_OID, p_eventoAutor_OIDs);

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
