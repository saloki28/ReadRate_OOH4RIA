
using System;
using System.Text;
using System.Collections.Generic;
using ReadRate_e4Gen.ApplicationCore.Exceptions;
using ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4;
using ReadRate_e4Gen.ApplicationCore.IRepository.ReadRate_E4;


/*PROTECTED REGION ID(usingReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Evento_obtenerListaParticipantes) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4
{
public partial class EventoCEN
{
public System.Collections.Generic.IList<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> ObtenerListaParticipantes (int p_oid)
{
        /*PROTECTED REGION ID(ReadRate_e4Gen.ApplicationCore.CEN.ReadRate_E4_Evento_obtenerListaParticipantes) ENABLED START*/

        EventoEN eventoEN = get_IEventoRepository ().DameEventoPorOID (p_oid);

        if (eventoEN == null) {
                throw new ModelException ("El evento con ID " + p_oid + " no existe.");
        }

        // Retornar la lista de participantes
        // Si es null, retornar lista vacía
        if (eventoEN.UsuarioParticipante == null) {
                return new System.Collections.Generic.List<ReadRate_e4Gen.ApplicationCore.EN.ReadRate_E4.UsuarioEN> ();
        }

        return eventoEN.UsuarioParticipante;

        /*PROTECTED REGION END*/
}
}
}
