
using System;
using System.Text;

using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4;



/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_desinscribirDeEvento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4
{
public partial class UsuarioCP : GenericBasicCP
{
public void DesinscribirDeEvento (int p_Usuario_OID, System.Collections.Generic.IList<int> p_evento_OIDs)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CP.ReadRate_E4_Usuario_desinscribirDeEvento) ENABLED START*/

        IUsuarioRepository usuarioRepository = null;
        IEventoRepository eventoRepository = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioRepository = CPSession.UnitRepo.UsuarioRepository;
                eventoRepository = CPSession.UnitRepo.EventoRepository;

                //diminuir aforo actual y validar inscripción
                foreach (int eventoId in p_evento_OIDs) { //evento a desinscribir
                        EventoEN eventoEN = eventoRepository.DameEventoPorOID (eventoId);

                        if (eventoEN.AforoActual <= 0) { //no hay gente inscrita
                                throw new ModelException ("No hay gente inscrita en el evento: " + eventoEN.Nombre);
                        }

                        // Obtener el evento con sus usuarios participantes para verificar inscripción
                        // Nota: Esto puede causar lazy loading de la colección UsuarioParticipante
                        bool usuarioInscrito = false;

                        // Intentar buscar en la colección de participantes
                        try {
                                if (eventoEN.UsuarioParticipante != null && eventoEN.UsuarioParticipante.Count > 0) {
                                        foreach (var participante in eventoEN.UsuarioParticipante) {
                                                if (participante.Id == p_Usuario_OID) {
                                                        usuarioInscrito = true;
                                                        break;
                                                }
                                        }
                                }
                        } catch (Exception) {
                                // Si falla al cargar la colección por polimorfismo, asumir que no está inscrito
                                // (esto no debería pasar si la relación está bien configurada)
                        }

                        if (!usuarioInscrito) {
                                throw new ModelException ("El usuario no está inscrito en el evento: " + eventoEN.Nombre);
                        }

                        eventoEN.AforoActual -= 1; //disminuir aforo actual
                }

                // Ejecutar la desinscripción
                usuarioRepository.DesinscribirDeEvento (p_Usuario_OID, p_evento_OIDs);

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
