
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_inscribirAEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class UsuarioCP : GenericBasicCP
{
public void InscribirAEvento (int p_Usuario_OID, System.Collections.Generic.IList<int> p_evento_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_inscribirAEvento) ENABLED START*/

        IUsuarioRepository usuarioRepository = null;
        IEventoRepository eventoRepository = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioRepository = CPSession.UnitRepo.UsuarioRepository;
                eventoRepository = CPSession.UnitRepo.EventoRepository;


                //comprobar aforo maximo, verificar que no está inscrito y aumentar aforo actual
                foreach (int eventoId in p_evento_OIDs) { //evento a inscribir
                        EventoEN eventoEN = eventoRepository.DameEventoPorOID (eventoId);

                        if (eventoEN.AforoActual >= eventoEN.AforoMax) { //aforo maximo alcanzado
                                throw new ModelException ("No se puede inscribir al evento " + eventoEN.Nombre + " porque ha alcanzado su aforo máximo.");
                        }

                        // Verificar si el usuario ya está inscrito en el evento
                        bool usuarioYaInscrito = false;

                        try {
                                if (eventoEN.UsuarioParticipante != null && eventoEN.UsuarioParticipante.Count > 0) {
                                        foreach (var participante in eventoEN.UsuarioParticipante) {
                                                if (participante.Id == p_Usuario_OID) {
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

                        eventoEN.AforoActual += 1; //aumentar aforo actual
                }

                // Ejecutar la inscripción
                usuarioRepository.InscribirAEvento (p_Usuario_OID, p_evento_OIDs);


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
